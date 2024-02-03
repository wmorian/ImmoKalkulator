using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services.Metric;

namespace kalkulator.tests.Services;

public class MetricsServiceTests
{
    private MetricsService _service;
    private double _livingSpace = 56;

    public MetricsServiceTests()
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
            },
            Forecast = new AnnualForecast
            {
                RentIncreasePercentage = 3,
                CostIncreasePercentage = 2,
                ValueIncreasePercentage = 2
            }
        };

        _service = new(calculation, _livingSpace);
    }

    [Fact]
    public void GetRentalYield_ReturnsCorrectCalculations()
    {
        // Act
        var result = _service.GetRentalYield();

        // Assert
        using (new AssertionScope())
        {
            result.ColdRentPerYear.Should().BeApproximately(10200, 1);
            result.GrossRentalYield.Should().BeApproximately(4.3, 0.1);
            result.PriceToRentRatio.Should().BeApproximately(23.3, 0.1);
            result.NetRentalYield.Should().BeApproximately(3.3, 0.1);
        }
    }

    [Fact]
    public void GetCashflow_ReturnsCorrectCalculations()
    {
        double personalTaxRate = 41; // Example tax rate

        // Act
        var result = _service.GetCashflow(personalTaxRate);

        // Assert
        using (new AssertionScope())
        {
            result.WarmRent.Should().BeApproximately(1012, 1);
            result.OperatingCosts.Should().BeApproximately(-287, 1);
            result.Interest.Should().BeApproximately(-795, 1);
            result.Repayment.Should().BeApproximately(-298, 1);
            result.OperatingCashflow.Should().BeApproximately(-368, 1);
            result.Tax.OperatingCostWithoutReserves.Should().BeApproximately(-210, 1);
            result.Tax.DepreciationAfa.Should().BeApproximately(-352, 1);
            result.Tax.TaxableCashflow.Should().BeApproximately(-345, 1);
            result.Tax.PersonalTaxRate.Should().Be(41);
            result.Tax.TaxesToPay.Should().BeApproximately(-142, 1);
            result.NetCashflow.Should().BeApproximately(-226, 1);
        }
    }

    [Fact]
    public void GetFinalYield_ReturnsCorrectCalculations()
    {
        double personalTaxRate = 41; // Example tax rate

        // Act
        var result = _service.GetFinalYield(2024, personalTaxRate);

        // Assert
        using (new AssertionScope())
        {
            result.CountYear.Should().Be(0);
            result.AssetGrowth.Should().BeApproximately(5616, 1);
            result.AssetGrowthWithoutAppreciation.Should().BeApproximately(856, 1);
            result.AnnualReturn.Should().BeApproximately(21.5, 0.5);
            result.AnnualReturnWithoutAppreciation.Should().BeApproximately(3.3, 0.1);
        }
    }
}