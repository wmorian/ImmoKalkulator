using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kalkulator.net.Model;
using Microsoft.Extensions.DependencyModel;

namespace kalkulator.net.Services;

// Rücklagen
public class ReservesService
{
    public ReservesCalcs GetReservesCalcs(Calculation calculation, double livingSpace)
    {
        var depreciation = calculation.Depreciation ?? throw new Exception($"{nameof(calculation.Depreciation)} cannot be null!");
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var operatingCosts = calculation.OperatingCosts ?? throw new Exception($"{nameof(calculation.OperatingCosts)} cannot be null!");

        var result = (depreciation.BuildingValuePercentageOfPurchasePrice / 100
            * purchaseDetail.PurchasePrice
            * (1 / 80 * 1.5) 
            - (12 * operatingCosts.HomeownersAssociationReserve))
            / livingSpace;

        return new ReservesCalcs { RecommendedMaintanceReserves = result };
    }
}

public class ReservesCalcs
{
    /*  
        Empfohlene Instandhalt.-Rückl.
        Bei einer eigenen Rückl. in dieser Höhe, ergibt sich zusammen mit der WEG-Rücklage 
        eine solide Gesamtrücklage gemäß der sog. Petersschen Formel. 
        Voraussetzung: Der Gebäudeanteil am Kaufpreis wurde mit der 
        entspr. Vorlage vom Finanzministerium ermittelt.  
    */
    public double RecommendedMaintanceReserves { get; set; }
}