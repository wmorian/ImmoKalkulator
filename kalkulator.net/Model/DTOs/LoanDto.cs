namespace kalkulator.net.Model.DTOs;

public class LoanDto
{
    public double LoanAmount { get; set; } // "Darlehenssumme"
    public double InterestRate { get; set; } // "Zinssatz"
    public double InitialRepaymentRate { get; set; } // "Anfängliche Tilgung"
    public double MonthlyPayment { get; set; } // "Kapitaldienst pro Monat"
    public int YearOfFullRepayment { get; set; } // "Jahr der Volltilgung"
}