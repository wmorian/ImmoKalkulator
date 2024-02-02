namespace kalkulator.net.Model.DTOs;

public class ForecastDto
{
    public decimal AnnualCostIncrease { get; set; } // "Kostensteigerung p.a."
    public decimal AnnualRentIncrease { get; set; } // "Mietsteigerung p.a."
    public decimal AnnualValueIncrease { get; set; } // "Wertsteigerung p.a."
}