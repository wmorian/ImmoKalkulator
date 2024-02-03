using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using kalkulator.net.Model;
using kalkulator.net.Services;

namespace kalkulator.tests;

public class LoanServiceTests
{
    private LoanService _service;

    public LoanServiceTests()
    {
        _service = new LoanService();
    }

    [Fact]
    public void GetLoanCalcs_WithSingleLoan_ReturnsCorrectCalculations()
    {
        // Arrange
        var calculation = new Calculation
        {
            PurchaseDetail = new PurchaseDetail { PurchasePrice = 238000 },
            Loans =
            [
                new Loan { LoanAmount = 238000, InterestRate = 4.01, InitialRepaymentRate = 1.5 }
            ]
        };
        var purchaseDetailCalcs = new PurchaseDetailCalcs { SumCharges = 26109 };
        var initialInvestmentCalcs = new InitialInvestmentCalcs { SumOfCosts = 0 };

        // Act
        var result = _service.GetLoanCalcs(calculation, purchaseDetailCalcs, initialInvestmentCalcs);

        // Assert
        using (new AssertionScope())
        {
            result.SumOfLoans.Should().Be(238000);
            result.Equity.Should().Be(26109); // PurchasePrice + SumCharges + SumOfCosts - SumOfLoans
            result.WeightedInterestRate.Should().BeApproximately(4.01, 0.01);
            result.WeightedRepaymentRate.Should().BeApproximately(1.5, 0.01);
        }
    }

    [Fact]
    public void GetLoanCalcs_WithMultipleLoans_ReturnsCorrectCalculations()
    {
        // Arrange
        var calculation = new Calculation
        {
            PurchaseDetail = new PurchaseDetail { PurchasePrice = 238000 },
            Loans = new List<Loan>
            {
                new Loan { LoanAmount = 214200, InterestRate = 4.01, InitialRepaymentRate = 1.5 },
                new Loan { LoanAmount = 50000, InterestRate = 2.0, InitialRepaymentRate = 2.0 }
            }
        };
        var purchaseDetailCalcs = new PurchaseDetailCalcs { SumCharges = 26109 };
        var initialInvestmentCalcs = new InitialInvestmentCalcs { SumOfCosts = 50000 };

        // Act
        var result = _service.GetLoanCalcs(calculation, purchaseDetailCalcs, initialInvestmentCalcs);

        // Assert
        using (new AssertionScope())
        {
            result.SumOfLoans.Should().Be(264200);
            result.Equity.Should().Be(49909); // PurchasePrice + SumCharges + SumOfCosts - SumOfLoans
            result.WeightedInterestRate.Should().BeApproximately(3.63, 0.01); // Weighted average of interest rates
            result.WeightedRepaymentRate.Should().BeApproximately(1.59, 0.01); // Weighted average of repayment rates
        }
    }
}