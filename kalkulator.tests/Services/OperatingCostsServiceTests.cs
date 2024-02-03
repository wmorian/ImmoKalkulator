using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services;

namespace kalkulator.tests;

public class OperatingCostsServiceTests
{
    private OperatingCostsService _service;

    public OperatingCostsServiceTests()
    {
        _service = new OperatingCostsService();
    }

    [Fact]
    public void GetOperatingCostsCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Arrange
        double livingSpace = 56;
        var calculation = new Calculation
        {
            OperatingCosts = new OperatingCosts
            {
                HousingAllowanceAllocable = 151,
                HousingAllowanceNonAllocable = 48,
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
                CalculatedRentLossPercentage = 3
            }
        };

        // Act
        var result = _service.GetOperatingCostsCalcs(calculation, livingSpace);

        // Assert
        using (new AssertionScope())
        {
            result.SumOperationCostsAllocable.Should().Be(162); // HousingAllowanceAllocable + PropertyTax + Allocable OtherCosts
            result.SumOfOperationCostsNonAllocable.Should().BeApproximately(125, 0.1); // HousingAllowanceNonAllocable + MaintenanceReserve + RentLoss + NonAllocable OtherCosts
            result.SumOfHousingAllowance.Should().Be(199); // HousingAllowanceAllocable + HousingAllowanceNonAllocable
            result.SumOfOperationCosts.Should().BeApproximately(287, 0.1); // SumOperationCostsAllocable + SumOfOperationCostsNonAllocable
        }
    }

    [Theory]
    [InlineData(nameof(Calculation.OperatingCosts))]
    [InlineData(nameof(Calculation.Rent))]
    [InlineData(nameof(Calculation.Reserves))]
    public void GetOperatingCostsCalcs_WithNullProperties_ThrowsException(string propertyName)
    {
        // Arrange
        var calculation = new Calculation
        {
            OperatingCosts = propertyName != nameof(Calculation.OperatingCosts) ? new OperatingCosts() : null,
            Rent = propertyName != nameof(Calculation.Rent) ? new Rent() : null,
            Reserves = propertyName != nameof(Calculation.Reserves) ? new Reserves() : null
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.GetOperatingCostsCalcs(calculation, 100));
        exception.Message.Should().Contain(propertyName);
    }
}
