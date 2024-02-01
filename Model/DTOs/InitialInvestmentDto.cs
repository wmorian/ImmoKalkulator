namespace kalkulator.net.Model.DTOs;

public class InitialInvestmentDto
{
    public string? Name { get; set; }
    public decimal Cost { get; set; }
    public TaxTreatment TaxTreatment { get; set; }
    public decimal ValueIncrease { get; set; }
}