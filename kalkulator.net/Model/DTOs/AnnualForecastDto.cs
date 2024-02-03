namespace kalkulator.net.Model.DTOs;

public class AnnualForecastDto
{
    public double CostIncreasePercentage { get; set; } // "Kostensteigerung p.a."
    public double RentIncreasePercentage { get; set; } // "Mietsteigerung p.a."
    public double ValueIncreasePercentage { get; set; } // "Wertsteigerung p.a."
}