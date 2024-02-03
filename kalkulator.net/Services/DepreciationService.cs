using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kalkulator.net.Model;

namespace kalkulator.net.Services;

public class DepreciationService
{
    public DepreciationCalcs GetDepreciationCalcs(Calculation calculation, PurchaseDetailCalcs purchaseDetailCalcs)
    {
        var depreciation = calculation.Depreciation ?? throw new Exception($"{nameof(calculation.Depreciation)} cannot be null!");
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var initialInvestments = calculation.InitialInvestments ?? throw new Exception($"{nameof(calculation.InitialInvestments)} cannot be null!");

        var result = depreciation.BuildingValuePercentageOfPurchasePrice / 100
            * (purchaseDetail.PurchasePrice + purchaseDetailCalcs.SumCharges)
            + initialInvestments.Where(i => i.TaxTreatment == TaxTreatment.Capitalize).Sum(i => i.Cost);

        return new DepreciationCalcs { LongtermDepreciation = result };
    }
}

public class DepreciationCalcs
{
    /*
        Basis langfristige Abschreibung
        Auf diese Basis bezieht sich die langfristige Abschreibung (AfA Satz). 
        Hierzu wird der Gebäudeanteil sowohl auf den Kaufpreis als auch auf die Kaufnebenkosten angewendet. 
        Hinzu kommen "aktivierte" anfängliche Investitionen. 
        =H20*(D14+E21)+SUMIF(D25:D28;"Aktivieren";C25:C28)
    */
    public double LongtermDepreciation { get; set; }
}