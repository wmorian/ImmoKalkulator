using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services;

namespace kalkulator.tests.Services;

public class RentServiceTests
{
    private RentService _service;

    public RentServiceTests()
    {
        _service = new RentService();
    }

    [Fact]
    public void GetRentCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Arrange
        var calculation = new Calculation
        {
            Rent = new Rent
            {
                TotalColdRent = 850,
                ParkingSpaces = 0,
                Other = 0
            },
            OperatingCosts = new OperatingCosts
            {
                HousingAllowanceAllocable = 151,
                PropertyTax = 11,
                OtherCosts =
                [
                    new() { Cost = 0, IsAllocable = true },
                    new() { Cost = 30, IsAllocable = false } // This should not be included in allocable costs
                ]
            }
        };

        // Act
        var result = _service.GetRentCalcs(calculation);

        // Assert
        using (new AssertionScope())
        {
            result.ColdRent.Should().Be(850); // TotalColdRent + ParkingSpaces + Other
            result.WarmRent.Should().Be(1012); // ColdRent + HousingAllowanceAllocable + PropertyTax + Allocable OtherCosts
        }
    }

    [Theory]
    [InlineData(nameof(Calculation.Rent))]
    [InlineData(nameof(Calculation.OperatingCosts))]
    public void GetRentCalcs_WithNullProperties_ThrowsException(string propertyName)
    {
        // Arrange
        var calculation = new Calculation
        {
            Rent = propertyName != nameof(Calculation.Rent) ? new Rent() : null,
            OperatingCosts = propertyName != nameof(Calculation.OperatingCosts) ? new OperatingCosts() : null
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.GetRentCalcs(calculation));
        exception.Message.Should().Contain(propertyName);
    }
}