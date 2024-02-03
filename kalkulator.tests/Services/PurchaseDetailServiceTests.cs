using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services;

namespace kalkulator.tests;

public class PurchaseDetailServiceTests
{
    private PurchaseDetailService _service;

    public PurchaseDetailServiceTests()
    {
        _service = new PurchaseDetailService();
    }

    [Fact]
    public void GetPurchaseDetailCalcs_WithValidInputs_ReturnsCorrectCalculations()
    {
        // Arrange
        var purchaseDetail = new PurchaseDetail
        {
            PurchasePrice = 238000,
            BrokerCommissionPercentage = 2.97,
            LandRegistryFeePercentage = 6,
            NotaryFeePercentage = 1.5,
            TransferTaxPercentage = 0.5,
            OtherCostsPercentage = 0
        };
        double livingSpace = 56;

        // Act
        var result = _service.GetPurchaseDetailCalcs(purchaseDetail, livingSpace);

        // Assert
        using (new AssertionScope())
        {
            result.PurchasePricePerQm.Should().BeApproximately(4250, 1); // PurchasePrice / livingSpace
            result.BrokerCommission.Should().BeApproximately(7069, 1); // PurchasePrice * BrokerCommissionPercentage / 100
            result.LandRegistryFee.Should().BeApproximately(14280, 1); // PurchasePrice * LandRegistryFeePercentage / 100
            result.NotaryFee.Should().BeApproximately(3570, 1); // PurchasePrice * NotaryFeePercentage / 100
            result.TransferTax.Should().BeApproximately(1190, 1); // PurchasePrice * TransferTaxPercentage / 100
            result.OtherCosts.Should().Be(0); // PurchasePrice * OtherCostsPercentage / 100
            result.SumChargesInPecent.Should().BeApproximately(10.97, 0.1); // Sum of all percentages
            result.SumCharges.Should().BeApproximately(26109, 1); // PurchasePrice * SumChargesInPecent / 100
        }
    }
}