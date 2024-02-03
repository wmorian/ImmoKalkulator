using kalkulator.net.Model;
using kalkulator.net.Services.Precalculation;

namespace kalkulator.net.Services.Metric;

public class MetricsService
{
    // private readonly PrecalculationsService _precalculationsService;
    private readonly Precalculations _precalculations;
    private readonly Calculation _calculation;

    public MetricsService(Calculation calculation, double livingSpace)
    {
        _calculation = calculation;
        var precalculationsService = new PrecalculationsService(_calculation, livingSpace);
        _precalculations = precalculationsService.GetPrecalculation();
    }

    public Metrics GetMetrics(double personalTaxRate, int year = 2024)
    {
        return new Metrics
        {
            RentalYield = GetRentalYield(),
            Cashflow = GetCashflow(personalTaxRate),
            FinalYield = GetFinalYield(year, personalTaxRate),
            Precalculations = _precalculations
        };
    }

    public RentalYield GetRentalYield()
    {
        var purchaseDetail = _calculation.PurchaseDetail ?? throw new Exception($"{nameof(_calculation.PurchaseDetail)} cannot be null!");

        var rentCalcs = _precalculations.RentCalcs ?? throw new Exception($"{nameof(_precalculations.RentCalcs)} cannot be null!");
        var operatingCalcs = _precalculations.OperatingCostsCalcs ?? throw new Exception($"{nameof(_precalculations.OperatingCostsCalcs)} cannot be null!");
        var initialInvestmentCalcs = _precalculations.InitialInvestmentCalcs ?? throw new Exception($"{nameof(_precalculations.InitialInvestmentCalcs)} cannot be null!");
        var purchaseDetailCalcs = _precalculations.PurchaseDetailCalcs ?? throw new Exception($"{nameof(_precalculations.PurchaseDetailCalcs)} cannot be null!");

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
        var depreciation = _calculation.Depreciation ?? throw new Exception($"{nameof(_calculation.Depreciation)} cannot be null!");

        var rentCalcs = _precalculations.RentCalcs ?? throw new Exception($"{nameof(_precalculations.RentCalcs)} cannot be null!");
        var operatingCalcs = _precalculations.OperatingCostsCalcs ?? throw new Exception($"{nameof(_precalculations.OperatingCostsCalcs)} cannot be null!");
        var loanCalcs = _precalculations.LoanCalcs ?? throw new Exception($"{nameof(_precalculations.LoanCalcs)} cannot be null!");
        var depreciationCalcs = _precalculations.DepreciationCalcs ?? throw new Exception($"{nameof(_precalculations.DepreciationCalcs)} cannot be null!");

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
        var forecast = _calculation.Forecast ?? throw new Exception($"{nameof(_calculation.Forecast)} cannot be null!");
        var initialInvestmentCalcs = _precalculations.InitialInvestmentCalcs ?? throw new Exception($"{nameof(_precalculations.InitialInvestmentCalcs)} cannot be null!");
        var loanCalcs = _precalculations.LoanCalcs ?? throw new Exception($"{nameof(_precalculations.LoanCalcs)} cannot be null!");

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
