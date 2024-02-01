namespace kalkulator.net.Model.DTOs;

public class DepreciationDto
{
    public decimal DepreciationRate { get; set; } // "AfA Satz" in German
    public decimal BuildingValuePercentageOfPurchasePrice { get; set; } // "Anteil Gebäude an Kaufpreis" in German
}