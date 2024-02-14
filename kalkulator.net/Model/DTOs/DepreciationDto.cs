namespace kalkulator.net.Model.DTOs;

public class DepreciationDto
{
    public double DepreciationRate { get; set; } // "AfA Satz"
    public double BuildingValuePercentageOfPurchasePrice { get; set; } // "Anteil Gebäude an Kaufpreis"
    public int Id { get; set; } // Primary key
    public int CalculationId { get; set; } // Foreign key to Calculation
}