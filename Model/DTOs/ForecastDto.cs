namespace kalkulator.net.Model.DTOs;

public class ForecastDto
{
    public decimal AnnualCostIncrease { get; set; } // "Kostensteigerung p.a." in German
    public decimal AnnualRentIncrease { get; set; } // "Mietsteigerung p.a." in German
    public decimal AnnualValueIncrease { get; set; } // "Wertsteigerung p.a." in German
}