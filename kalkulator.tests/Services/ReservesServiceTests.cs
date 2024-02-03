using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services;
using Xunit.Abstractions;

namespace kalkulator.tests.Services;

public class ReservesServiceTests
{
    private readonly ITestOutputHelper _output;
    private ReservesService _service;

    public ReservesServiceTests(ITestOutputHelper output)
    {
        _service = new ReservesService();
        _output = output;
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
            result.RecommendedMaintanceReserves.Should().Be(47);
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