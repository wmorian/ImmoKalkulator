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