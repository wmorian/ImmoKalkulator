using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services;

namespace kalkulator.tests.Services;

public class ReservesServiceTests
{
    private ReservesService _service;

    public ReservesServiceTests()
    {
        _service = new ReservesService();
    }

    [Fact]
    public void GetReservesCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Arrange
        double livingSpace = 56;
        var calculation = new Calculation
        {
            Depreciation = new Depreciation { BuildingValuePercentageOfPurchasePrice = 80 },
            PurchaseDetail = new PurchaseDetail { PurchasePrice = 238000 },
            OperatingCosts = new OperatingCosts { HomeownersAssociationReserve = 79 }
        };

        // Act
        var result = _service.GetReservesCalcs(calculation, livingSpace);

        // Assert
        using (new AssertionScope())
        {
            result.RecommendedMaintanceReserves.Should().BeApproximately(47, 0.01);
        }
    }

    [Theory]
    [InlineData(nameof(Calculation.Depreciation))]
    [InlineData(nameof(Calculation.PurchaseDetail))]
    [InlineData(nameof(Calculation.OperatingCosts))]
    public void GetReservesCalcs_WithNullProperties_ThrowsException(string propertyName)
    {
        // Arrange
        var calculation = new Calculation
        {
            Depreciation = propertyName != nameof(Calculation.Depreciation) ? new Depreciation() : null,
            PurchaseDetail = propertyName != nameof(Calculation.PurchaseDetail) ? new PurchaseDetail() : null,
            OperatingCosts = propertyName != nameof(Calculation.OperatingCosts) ? new OperatingCosts() : null
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.GetReservesCalcs(calculation, 100));
        exception.Message.Should().Contain(propertyName);
    }
}