namespace kalkulator.net.Model;

public class Loan
{
    public int Id { get; set; } // Primary key
    public decimal LoanAmount { get; set; } // "Darlehenssumme" in German
    public decimal InterestRate { get; set; } // "Zinssatz" in German
    public decimal InitialRepaymentRate { get; set; } // "Anfängliche Tilgung" in German
    public decimal MonthlyPayment { get; set; } // "Kapitaldienst pro Monat" in German
    public int YearOfFullRepayment { get; set; } // "Jahr der Volltilgung" in German

    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}