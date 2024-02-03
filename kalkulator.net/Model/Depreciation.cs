namespace kalkulator.net.Model;

// Abschreibung
public class Depreciation
{
    public int Id { get; set; } // Primary key
    public double DepreciationRate { get; set; } // "AfA Satz"
    public double BuildingValuePercentageOfPurchasePrice { get; set; } // "Anteil Gebäude an Kaufpreis"

    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}