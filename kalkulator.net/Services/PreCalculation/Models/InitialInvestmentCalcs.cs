using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kalkulator.net.Model;

namespace kalkulator.net.Services.Precalculation;

// Anfängliche Investitionen

public class InitialInvestmentCalcs
{
    public double SumOfCosts { get; set; }
    public double SumOfValueIncreases { get; set; }
    public double NewPropertyValue { get; set; } /*  Neuer Wert: Der "neue" Wert der Immobilie setzt sich aus dem Kaufpreis und den erfassten wertsteigernden Investitionen zusammen. Dies ist der rechnerische Ausgangspunkt für die Prognose der weiteren Wertentwicklung.  */
    public double LimitOf15Percent { get; set; } /*  15% Grenze: Wenn die Investitionen in den ersten 3 Jahren über diesem Wert liegen, stellen sie "anschaffungsnahen Herstellungsaufwand" dar und dürfen steuerlich nur noch langfristig abgeschrieben werden (--> "aktivieren"). Bitte unbedingt Steuerberater einbinden.
*/
}