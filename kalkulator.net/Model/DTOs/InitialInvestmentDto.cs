namespace kalkulator.net.Model.DTOs;

public class InitialInvestmentDto
{
    public string? Name { get; set; }
    public double Cost { get; set; }
    public TaxTreatment TaxTreatment { get; set; }
    public double ValueIncrease { get; set; }
}