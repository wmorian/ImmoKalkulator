namespace kalkulator.net.Model.DTOs;

public class LoanDto
{
    public decimal LoanAmount { get; set; } // "Darlehenssumme"
    public decimal InterestRate { get; set; } // "Zinssatz"
    public decimal InitialRepaymentRate { get; set; } // "Anfängliche Tilgung"
    public decimal MonthlyPayment { get; set; } // "Kapitaldienst pro Monat"
    public int YearOfFullRepayment { get; set; } // "Jahr der Volltilgung"
}