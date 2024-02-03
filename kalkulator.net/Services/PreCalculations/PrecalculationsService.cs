using kalkulator.net.Model;

namespace kalkulator.net.Services.PreCalculations;

public class PrecalculationsService(Calculation calculation, double livingSpace)
{
    // Abschreibung
    public DepreciationCalcs GetDepreciationCalcs()
    {
        var depreciation = calculation.Depreciation ?? throw new Exception($"{nameof(calculation.Depreciation)} cannot be null!");
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var initialInvestments = calculation.InitialInvestments ?? throw new Exception($"{nameof(calculation.InitialInvestments)} cannot be null!");
        var purchaseDetailCalcs = GetPurchaseDetailCalcs();


        var result = depreciation.BuildingValuePercentageOfPurchasePrice / 100
            * (purchaseDetail.PurchasePrice + purchaseDetailCalcs.SumCharges)
            + initialInvestments.Where(i => i.TaxTreatment == TaxTreatment.Capitalize).Sum(i => i.Cost);

        return new DepreciationCalcs { LongtermDepreciation = result };
    }

    // Anfängliche Investitionen
    public InitialInvestmentCalcs GetInitialInvestmentCalcs()
    {
        var initialInvestments = calculation.InitialInvestments;
        var purchasePrice = calculation.PurchaseDetail?.PurchasePrice ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var depreciation = calculation.Depreciation ?? throw new Exception($"{nameof(calculation.Depreciation)} cannot be null!");
        var purchaseDetailCalcs = GetPurchaseDetailCalcs();

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

    // Darlehen
    public LoanCalcs GetLoanCalcs()
    {
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var loans = calculation.Loans ?? throw new Exception($"{nameof(calculation.Loans)} cannot be null!");
        var purchaseDetailCalcs = GetPurchaseDetailCalcs();
        var initialInvestmentCalcs = GetInitialInvestmentCalcs();

        var sumOfLoans = loans.Sum(l => l.LoanAmount);
        var overallInvestment = purchaseDetail.PurchasePrice + purchaseDetailCalcs.SumCharges + initialInvestmentCalcs.SumOfCosts;
        var equity = overallInvestment - sumOfLoans;

        double weightedInterest = 0.0;
        double weightedRepayment = 0.0;
        if (sumOfLoans > 0)
        {
            weightedInterest = loans.Sum(l => l.LoanAmount * l.InterestRate / 100) / sumOfLoans;
            weightedRepayment = loans.Sum(l => l.LoanAmount * l.InitialRepaymentRate / 100) / sumOfLoans;
        }

        double payments = loans.Sum(l => ((l.InitialRepaymentRate / 100) + (l.InitialRepaymentRate / 100)) * l.LoanAmount / 12);

        return new LoanCalcs
        {
            SumOfLoans = sumOfLoans,
            Equity = equity,
            WeightedInterestRate = weightedInterest * 100,
            WeightedRepaymentRate = weightedRepayment * 100
        };
    }

    // Bewirtschaftungskosten
    public OperatingCostsCalcs GetOperatingCostsCalcs()
    {
        var operatingCosts = calculation.OperatingCosts ?? throw new Exception($"{nameof(calculation.OperatingCosts)} cannot be null!");
        var rent = calculation.Rent ?? throw new Exception($"{nameof(calculation.Rent)} cannot be null!");
        var reserves = calculation.Reserves ?? throw new Exception($"{nameof(calculation.Reserves)} cannot be null!");

        var sumOperationCostsAllocable = operatingCosts.HousingAllowanceAllocable
            + operatingCosts.PropertyTax
            + operatingCosts.OtherCosts.Where(c => c.IsAllocable).Sum(c => c.Cost);

        var warmRent = rent.TotalColdRent + rent.ParkingSpaces + rent.Other + sumOperationCostsAllocable;
        var sumOfOperationCostsNonAllocable = operatingCosts.HousingAllowanceNonAllocable
            + (livingSpace * reserves.MaintenanceReservePerSquareMeterPerAnnum / 12)
            + (reserves.CalculatedRentLossPercentage / 100 * warmRent)
            + operatingCosts.OtherCosts.Where(c => !c.IsAllocable).Sum(c => c.Cost);

        return new OperatingCostsCalcs
        {
            SumOperationCostsAllocable = sumOperationCostsAllocable,
            SumOfOperationCostsNonAllocable = sumOfOperationCostsNonAllocable,
            SumOfHousingAllowance = operatingCosts.HousingAllowanceAllocable + operatingCosts.HousingAllowanceNonAllocable,
            SumOfOperationCosts = sumOperationCostsAllocable + sumOfOperationCostsNonAllocable
        };
    }

    // Kaufpreis und Kaufnebenkosten
    public PurchaseDetailCalcs GetPurchaseDetailCalcs()
    {
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");

        var sumChargesInPercent = purchaseDetail.BrokerCommissionPercentage
            + purchaseDetail.LandRegistryFeePercentage
            + purchaseDetail.NotaryFeePercentage
            + purchaseDetail.TransferTaxPercentage
            + purchaseDetail.OtherCostsPercentage;

        return new PurchaseDetailCalcs
        {
            PurchasePricePerQm = purchaseDetail.PurchasePrice / livingSpace,
            BrokerCommission = purchaseDetail.PurchasePrice * purchaseDetail.BrokerCommissionPercentage / 100,
            LandRegistryFee = purchaseDetail.PurchasePrice * purchaseDetail.LandRegistryFeePercentage / 100,
            NotaryFee = purchaseDetail.PurchasePrice * purchaseDetail.NotaryFeePercentage / 100,
            TransferTax = purchaseDetail.PurchasePrice * purchaseDetail.TransferTaxPercentage / 100,
            OtherCosts = purchaseDetail.PurchasePrice * purchaseDetail.OtherCostsPercentage / 100,
            SumChargesInPecent = sumChargesInPercent,
            SumCharges = purchaseDetail.PurchasePrice * sumChargesInPercent / 100
        };
    }


    // Miete
    public RentCalcs GetRentCalcs()
    {
        var rent = calculation.Rent ?? throw new Exception($"{nameof(calculation.Rent)} cannot be null!");
        var operatingCosts = calculation.OperatingCosts ?? throw new Exception($"{nameof(calculation.OperatingCosts)} cannot be null!");

        double coldRent = rent.TotalColdRent + rent.ParkingSpaces + rent.Other;
        var sumOperationCostsAllocable = operatingCosts.HousingAllowanceAllocable
            + operatingCosts.PropertyTax
            + operatingCosts.OtherCosts.Where(c => c.IsAllocable).Sum(c => c.Cost);

        return new RentCalcs
        {
            ColdRent = Math.Round(coldRent),
            WarmRent = Math.Round(coldRent + sumOperationCostsAllocable)
        };
    }

    // Rücklagen
    public ReservesCalcs GetReservesCalcs()
    {
        var depreciation = calculation.Depreciation ?? throw new Exception($"{nameof(calculation.Depreciation)} cannot be null!");
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var operatingCosts = calculation.OperatingCosts ?? throw new Exception($"{nameof(calculation.OperatingCosts)} cannot be null!");

        var result = (depreciation.BuildingValuePercentageOfPurchasePrice / 100
            * purchaseDetail.PurchasePrice
            * (1.0 / 80.0 * 1.5)
            - (12 * operatingCosts.HomeownersAssociationReserve))
            / livingSpace;

        return new ReservesCalcs { RecommendedMaintanceReserves = Math.Round(result) };
    }
}