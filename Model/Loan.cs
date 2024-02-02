namespace kalkulator.net.Model;

public class Loan
{
    public int Id { get; set; } // Primary key
    public decimal LoanAmount { get; set; } // "Darlehenssumme"
    public decimal InterestRate { get; set; } // "Zinssatz"
    public decimal InitialRepaymentRate { get; set; } // "Anfängliche Tilgung"
    public decimal MonthlyPayment { get; set; } // "Kapitaldienst pro Monat"
    public int YearOfFullRepayment { get; set; } // "Jahr der Volltilgung"

    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}