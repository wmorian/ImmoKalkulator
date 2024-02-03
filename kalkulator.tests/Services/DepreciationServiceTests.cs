using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using kalkulator.net.Model;
using kalkulator.net.Services;

namespace kalkulator.net.Tests;

public class DepreciationServiceTests
{
    [Fact]
    public void GetDepreciationCalcs_WithValidInputs_ReturnsCorrectCalculation()
    {
        // Arrange
        var service = new DepreciationService();
        var calculation = new Calculation
        {
            Depreciation = new Depreciation { BuildingValuePercentageOfPurchasePrice = 80 },
            PurchaseDetail = new PurchaseDetail { PurchasePrice = 238000 },
            InitialInvestments = []
        };
        var purchaseDetailCalcs = new PurchaseDetailCalcs { SumCharges = 26109 };

        var expectedLongtermDepreciation = 211287; // 20% of (PurchasePrice + SumCharges) + Sum of Capitalize Costs

        // Act
        var result = service.GetDepreciationCalcs(calculation, purchaseDetailCalcs);

        // Assert
        result.LongtermDepreciation.Should().BeApproximately(expectedLongtermDepreciation, 1);
        // Assert.Equal(expectedLongtermDepreciation, result.LongtermDepreciation);
    }

    [Theory]
    [InlineData(nameof(Calculation.Depreciation))]
    [InlineData(nameof(Calculation.PurchaseDetail))]
    public void GetDepreciationCalcs_WithNullProperties_ThrowsException(string propertyName)
    {
        // Arrange
        var service = new DepreciationService();
        var calculation = new Calculation
        {
            Depreciation = propertyName != nameof(Calculation.Depreciation) ? new Depreciation() : null,
            PurchaseDetail = propertyName != nameof(Calculation.PurchaseDetail) ? new PurchaseDetail() : null,
        };
        var purchaseDetailCalcs = new PurchaseDetailCalcs();

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => service.GetDepreciationCalcs(calculation, purchaseDetailCalcs));
        Assert.Contains(propertyName, exception.Message);
    }
}