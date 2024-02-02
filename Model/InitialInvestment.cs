namespace kalkulator.net.Model;

public class InitialInvestment
{
    public int Id { get; set; } // Primary key
    public int CalculationId { get; set; } // Foreign key to Calculation
    public Calculation? Calculation { get; set; } // Navigation property back to Calculation
    public string? Name { get; set; }
    public decimal Cost { get; set; }
    public TaxTreatment TaxTreatment { get; set; }
    public decimal ValueIncrease { get; set; }
}

public enum TaxTreatment
{
    Capitalize, // "Aktivieren"
    ExpenseImmediately // "Sofort"
}