using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services.PreCalculations;

namespace kalkulator.tests.Services;

public class PrecalculationsServiceTests
{
    private PrecalculationsService _service;
    private double _livingSpace = 56;

    public PrecalculationsServiceTests()
    {
        var calculation = new Calculation
        {
            Depreciation = new Depreciation
            {
                BuildingValuePercentageOfPurchasePrice = 80,
                DepreciationRate = 2
            },
            PurchaseDetail = new PurchaseDetail
            {
                PurchasePrice = 238000,
                BrokerCommissionPercentage = 2.97,
                LandRegistryFeePercentage = 6,
                NotaryFeePercentage = 1.5,
                TransferTaxPercentage = 0.5,
                OtherCostsPercentage = 0
            },
            InitialInvestments = [],
            Loans =
            [
                new Loan { LoanAmount = 238000, InterestRate = 4.01, InitialRepaymentRate = 1.5 }
            ],
            OperatingCosts = new OperatingCosts
            {
                HousingAllowanceAllocable = 151,
                HousingAllowanceNonAllocable = 48,
                HomeownersAssociationReserve = 79,
                PropertyTax = 11,
                OtherCosts = []
            },
            Rent = new Rent
            {
                TotalColdRent = 850,
                ParkingSpaces = 0,
                Other = 0
            },
            Reserves = new Reserves
            {
                MaintenanceReservePerSquareMeterPerAnnum = 10,
                CalculatedRentLossPercentage = 3,
            }
        };

        _service = new(calculation, _livingSpace);
    }

    [Fact]
    public void GetDepreciationCalcs_WithValidInputs_ReturnsCorrectCalculation()
    {
        // Act
        var result = _service.GetDepreciationCalcs();

        // Assert
        result.LongtermDepreciation.Should().BeApproximately(211287, 1);
    }

    [Fact]
    public void GetInitialInvestmentCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Act
        var result = _service.GetInitialInvestmentCalcs();

        // Assert
        result.SumOfCosts.Should().Be(0); // Sum of Costs
        result.SumOfValueIncreases.Should().Be(0); // Sum of Value Increases
        result.NewPropertyValue.Should().Be(238000); // New Property Value
        result.LimitOf15Percent.Should().BeApproximately(37715, 1); // Limit of 15 Percent with a precision of 0.1
    }

    [Fact]
    public void GetLoanCalcs_WithSingleLoan_ReturnsCorrectCalculations()
    {
        // Act
        var result = _service.GetLoanCalcs();

        // Assert
        using (new AssertionScope())
        {
            result.SumOfLoans.Should().Be(238000);
            result.Equity.Should().BeApproximately(26109, 1); // PurchasePrice + SumCharges + SumOfCosts - SumOfLoans
            result.WeightedInterestRate.Should().BeApproximately(4.01, 0.01);
            result.WeightedRepaymentRate.Should().BeApproximately(1.5, 0.01);
        }
    }

    [Fact]
    public void GetOperatingCostsCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Act
        var result = _service.GetOperatingCostsCalcs();

        // Assert
        using (new AssertionScope())
        {
            result.SumOperationCostsAllocable.Should().Be(162); // HousingAllowanceAllocable + PropertyTax + Allocable OtherCosts
            result.SumOfOperationCostsNonAllocable.Should().BeApproximately(125, 0.1); // HousingAllowanceNonAllocable + MaintenanceReserve + RentLoss + NonAllocable OtherCosts
            result.SumOfHousingAllowance.Should().Be(199); // HousingAllowanceAllocable + HousingAllowanceNonAllocable
            result.SumOfOperationCosts.Should().BeApproximately(287, 0.1); // SumOperationCostsAllocable + SumOfOperationCostsNonAllocable
        }
    }

    [Fact]
    public void GetPurchaseDetailCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Act
        var result = _service.GetPurchaseDetailCalcs();

        // Assert
        using (new AssertionScope())
        {
            result.PurchasePricePerQm.Should().BeApproximately(4250, 1); // PurchasePrice / livingSpace
            result.BrokerCommission.Should().BeApproximately(7069, 1); // PurchasePrice * BrokerCommissionPercentage / 100
            result.NotaryFee.Should().BeApproximately(3570, 1); // PurchasePrice * NotaryFeePercentage / 100
            result.TransferTax.Should().BeApproximately(1190, 1); // PurchasePrice * TransferTaxPercentage / 100
            result.LandRegistryFee.Should().BeApproximately(14280, 1); // PurchasePrice * LandRegistryFeePercentage / 100
            result.OtherCosts.Should().Be(0); // PurchasePrice * OtherCostsPercentage / 100
            result.SumChargesInPecent.Should().BeApproximately(10.97, 0.1); // Sum of all percentages
            result.SumCharges.Should().BeApproximately(26109, 1); // PurchasePrice * SumChargesInPecent / 100
        }
    }

    [Fact]
    public void GetRentCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Act
        var result = _service.GetRentCalcs();

        // Assert
        using (new AssertionScope())
        {
            result.ColdRent.Should().Be(850); // TotalColdRent + ParkingSpaces + Other
            result.WarmRent.Should().Be(1012); // ColdRent + HousingAllowanceAllocable + PropertyTax + Allocable OtherCosts
        }
    }

    [Fact]
    public void GetReservesCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Act
        var result = _service.GetReservesCalcs();

        // Assert
        using (new AssertionScope())
        {
            result.RecommendedMaintanceReserves.Should().Be(47);
        }
    }
}