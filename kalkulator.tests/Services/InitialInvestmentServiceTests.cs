using FluentAssertions;
using kalkulator.net.Model;
using kalkulator.net.Services;

namespace kalkulator.tests;

public class InitialInvestmentServiceTests
{
    [Fact]
    public void GetInitialInvestmentCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Arrange
        var service = new InitialInvestmentService();
        var calculation = new Calculation
        {
            InitialInvestments =
            [
                new() { Cost = 3000, ValueIncrease = 2000 },
                new() { Cost = 5000, ValueIncrease = 4000 }
            ],
            PurchaseDetail = new PurchaseDetail { PurchasePrice = 238000 },
            Depreciation = new Depreciation { BuildingValuePercentageOfPurchasePrice = 80 }
        };
        var purchaseDetailCalcs = new PurchaseDetailCalcs { SumCharges = 26109 };

        // Act
        var result = service.GetInitialInvestmentCalcs(calculation, purchaseDetailCalcs);

        // Assert
        result.SumOfCosts.Should().Be(8000); // Sum of Costs
        result.SumOfValueIncreases.Should().Be(6000); // Sum of Value Increases
        result.NewPropertyValue.Should().Be(244000); // New Property Value
        result.LimitOf15Percent.Should().BeApproximately(37715, 1); // Limit of 15 Percent with a precision of 0.1
    }

    [Theory]
    [InlineData(nameof(Calculation.PurchaseDetail))]
    [InlineData(nameof(Calculation.Depreciation))]
    public void GetInitialInvestmentCalcs_WithNullProperties_ThrowsException(string propertyName)
    {
        // Arrange
        var service = new InitialInvestmentService();
        var calculation = new Calculation
        {
            InitialInvestments = [],
            PurchaseDetail = propertyName != nameof(Calculation.PurchaseDetail) ? new PurchaseDetail { PurchasePrice = 1000 } : null,
            Depreciation = propertyName != nameof(Calculation.Depreciation) ? new Depreciation { BuildingValuePercentageOfPurchasePrice = 20 } : null
        };
        var purchaseDetailCalcs = new PurchaseDetailCalcs();

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => service.GetInitialInvestmentCalcs(calculation, purchaseDetailCalcs));
        Assert.Contains(propertyName, exception.Message);
    }
}
