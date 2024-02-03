using kalkulator.net.Model;
using kalkulator.net.Services.Precalculation;

namespace kalkulator.net.Services.Metrics;

public class MetricsService(Calculation calculation, double livingSpace)
{
    private readonly PrecalculationsService _precalculationsService = new(calculation, livingSpace);

    public RentalYield GetRentalYield()
    {
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");

        var rentCalcs = _precalculationsService.GetRentCalcs();
        var operatingCalcs = _precalculationsService.GetOperatingCostsCalcs();
        var initialInvestmentCalcs = _precalculationsService.GetInitialInvestmentCalcs();
        var purchaseDetailCalcs = _precalculationsService.GetPurchaseDetailCalcs();

        double overallInvestment = purchaseDetail.PurchasePrice + purchaseDetailCalcs.SumCharges + initialInvestmentCalcs.SumOfCosts;

        double coldRentPerYear = rentCalcs.ColdRent * 12;
        double grossRentalYield = coldRentPerYear / purchaseDetail.PurchasePrice * 100;
        double priceToRentRatio = purchaseDetail.PurchasePrice / coldRentPerYear;
        double netRentalYield = (coldRentPerYear - (12 * operatingCalcs.SumOfOperationCostsNonAllocable)) / overallInvestment * 100;

        return new RentalYield
        {
            ColdRentPerYear = coldRentPerYear,
            GrossRentalYield = grossRentalYield,
            PriceToRentRatio = priceToRentRatio,
            NetRentalYield = netRentalYield
        };
    }

    public Cashflow GetCashflow(double personalTaxRate)
    {
        var depreciation = calculation.Depreciation ?? throw new Exception($"{nameof(calculation.Depreciation)} cannot be null!");

        var rentCalcs = _precalculationsService.GetRentCalcs();
        var operatingCalcs = _precalculationsService.GetOperatingCostsCalcs();
        var loanCalcs = _precalculationsService.GetLoanCalcs();
        var depreciationCalcs = _precalculationsService.GetDepreciationCalcs();

        double warmRent = rentCalcs.WarmRent;
        double operatingCosts = -operatingCalcs.SumOfOperationCosts;
        double interest = -(loanCalcs.WeightedInterestRate / 100 * loanCalcs.SumOfLoans / 12);
        double repayment = -(loanCalcs.WeightedRepaymentRate / 100 * loanCalcs.SumOfLoans / 12);
        double operatingCostWithoutReserves = operatingCosts + operatingCalcs.PersonalMaintenanceReserves + operatingCalcs.CalculatedRentLoss;
        double depreciationAfa = -(depreciation.DepreciationRate / 100 * depreciationCalcs.LongtermDepreciation / 12);
        double taxableCashflow = warmRent + operatingCostWithoutReserves + interest + depreciationAfa;
        Tax tax = new()
        {
            OperatingCostWithoutReserves = operatingCostWithoutReserves,
            DepreciationAfa = depreciationAfa,
            TaxableCashflow = taxableCashflow,
            PersonalTaxRate = personalTaxRate,
            TaxesToPay = personalTaxRate / 100 * taxableCashflow
        };
        double operatingCashflow = warmRent + operatingCosts + interest + repayment;

        return new Cashflow
        {
            WarmRent = warmRent,
            OperatingCosts = operatingCosts,
            Interest = interest,
            Repayment = repayment,
            OperatingCashflow = operatingCashflow,
            Tax = tax,
            NetCashflow = operatingCashflow - tax.TaxesToPay
        };
    }

    public FinalYield GetFinalYield(int year, double personalTaxRate)
    {
        var forecast = calculation.Forecast ?? throw new Exception($"{nameof(calculation.Forecast)} cannot be null!");
        var initialInvestmentCalcs = _precalculationsService.GetInitialInvestmentCalcs();

        var loanCalcs = _precalculationsService.GetLoanCalcs();
        var casflow = GetCashflow(personalTaxRate);
        double valueIncreaseFirstYear = forecast.ValueIncreasePercentage / 100 * initialInvestmentCalcs.NewPropertyValue;
        double assetGrowthWithoutAppreciation = 12 * (-casflow.Repayment + casflow.NetCashflow);
        double assetGrowth = assetGrowthWithoutAppreciation + valueIncreaseFirstYear;
        double annualReturn = loanCalcs.Equity > 0 ? (assetGrowth / loanCalcs.Equity * 100) : 0;
        double annualReturnWithoutAppreciation = loanCalcs.Equity > 0 ? (assetGrowthWithoutAppreciation / loanCalcs.Equity * 100) : 0;

        return new FinalYield
        {
            CountYear = year - DateTime.Now.Year,
            AssetGrowth = assetGrowth,
            AssetGrowthWithoutAppreciation = assetGrowthWithoutAppreciation,
            AnnualReturn = annualReturn,
            AnnualReturnWithoutAppreciation = annualReturnWithoutAppreciation
        };
    }
}

public class RentalYield
{
    public double ColdRentPerYear { get; set; } // Nettokaltmiete
    public double GrossRentalYield { get; set; } // Bruttomietrendite
    public double PriceToRentRatio { get; set; } // Kaufpreisfaktor
    public double NetRentalYield { get; set; } // Nettomietrendite
}

public class Cashflow
{
    public double WarmRent { get; set; }
    public double OperatingCosts { get; set; }
    public double Interest { get; set; }
    public double Repayment { get; set; }
    public double OperatingCashflow { get; set; }
    public Tax Tax { get; set; } = new();
    public double NetCashflow { get; set; }
}

public class Tax
{
    public double OperatingCostWithoutReserves { get; set; }
    public double DepreciationAfa { get; set; }
    public double TaxableCashflow { get; set; }
    public double PersonalTaxRate { get; set; }
    public double TaxesToPay { get; set; }
}

public class FinalYield
{
    public int CountYear { get; set; } 
    public double AssetGrowth { get; set; } // Vermögenszuwachs p.a. 
    public double AssetGrowthWithoutAppreciation { get; set; } // ohne Wertsteigerung
    public double AnnualReturn { get; set; } // Eigenkapitalrendite p.a.
    public double AnnualReturnWithoutAppreciation { get; set; } // ohne Wertsteigerung
}