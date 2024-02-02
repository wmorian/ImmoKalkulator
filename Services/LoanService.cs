using kalkulator.net.Model;

namespace kalkulator.net.Services;

public class LoanService
{
    public LoanCalcs GetLoanCalcs(Calculation calculation, PurchaseDetailCalcs purchaseDetailCalcs, InitialInvestmentCalcs initialInvestmentCalcs)
    {
        var purchaseDetail = calculation.PurchaseDetail ?? throw new Exception($"{nameof(calculation.PurchaseDetail)} cannot be null!");
        var loans = calculation.Loans ?? throw new Exception($"{nameof(calculation.Loans)} cannot be null!");

        var sumOfLoans = loans.Sum(l => l.LoanAmount);
        var overallInvestment = purchaseDetail.PurchasePrice + purchaseDetailCalcs.SumCharges + initialInvestmentCalcs.SumOfCosts;
        var equity = overallInvestment - sumOfLoans;

        double weightedInterest = 0.0;
        double weightedRepayment = 0.0;
        if (sumOfLoans > 0)
        {
            weightedInterest = loans.Sum(l => l.LoanAmount * l.InterestRate / 100) / sumOfLoans;
            weightedRepayment = loans.Sum(l => l.LoanAmount * l.InitialRepaymentRate / 100) / sumOfLoans;
        }

        double payments = loans.Sum(l => ((l.InitialRepaymentRate / 100) + (l.InitialRepaymentRate / 100)) * l.LoanAmount / 12 );

        return new LoanCalcs
        {
            SumOfLoans = sumOfLoans,
            Equity = equity,
            WeightedInterestRate = weightedInterest,
            WeightedRepaymentRate = weightedRepayment
        };
    }
}

public class LoanCalcs
{
    public double SumOfLoans { get; set; }
    public double Equity { get; set; }
    public double WeightedInterestRate { get; set; }
    public double WeightedRepaymentRate { get; set; }
    public double SumOfMonthlyPayments { get; set; }

}