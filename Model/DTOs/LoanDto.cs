namespace kalkulator.net.Model.DTOs;

public class LoanDto
{
    public decimal LoanAmount { get; set; } // "Darlehenssumme" in German
    public decimal InterestRate { get; set; } // "Zinssatz" in German
    public decimal InitialRepaymentRate { get; set; } // "Anfängliche Tilgung" in German
    public decimal MonthlyPayment { get; set; } // "Kapitaldienst pro Monat" in German
    public int YearOfFullRepayment { get; set; } // "Jahr der Volltilgung" in German
}