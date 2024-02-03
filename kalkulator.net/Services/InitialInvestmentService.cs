using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kalkulator.net.Model;

namespace kalkulator.net.Services;

// Anfängliche Investitionen
public class InitialInvestmentService
{
    public InitialInvestmentCalcs GetInitialInvestmentCalcs(Calculation calculation, PurchaseDetailCalcs purchaseDetailCalcs)
    {
        var initialInvestments = calculation.InitialInvestments;
        var purchasePrice = calculation.PurchaseDetail?.PurchasePrice ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var depreciation = calculation.Depreciation ?? throw new Exception($"{nameof(calculation.Depreciation)} cannot be null!");

        double sumOfCosts = initialInvestments.Sum(i => i.Cost);
        double sumOfValueIncreases = initialInvestments.Sum(i => i.ValueIncrease);
        
        return new InitialInvestmentCalcs 
        {
            SumOfCosts = sumOfCosts,
            SumOfValueIncreases = sumOfValueIncreases,
            NewPropertyValue = purchasePrice + sumOfValueIncreases,
            LimitOf15Percent = 0.15 * (depreciation.BuildingValuePercentageOfPurchasePrice / 100) 
                * (purchasePrice + purchaseDetailCalcs.SumCharges)
                * 1.19 
        };
    }
}

public class InitialInvestmentCalcs
{
    public double SumOfCosts { get; set; }
    public double SumOfValueIncreases { get; set; }
    public double NewPropertyValue { get; set; } /*  Neuer Wert: Der "neue" Wert der Immobilie setzt sich aus dem Kaufpreis und den erfassten wertsteigernden Investitionen zusammen. Dies ist der rechnerische Ausgangspunkt für die Prognose der weiteren Wertentwicklung.  */
    public double LimitOf15Percent { get; set; } /*  15% Grenze: Wenn die Investitionen in den ersten 3 Jahren über diesem Wert liegen, stellen sie "anschaffungsnahen Herstellungsaufwand" dar und dürfen steuerlich nur noch langfristig abgeschrieben werden (--> "aktivieren"). Bitte unbedingt Steuerberater einbinden.
*/
}