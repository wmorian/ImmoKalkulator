namespace kalkulator.net.Model.DTOs;

public class CalculationDto
{
    public int PropertyId { get; set; } // Foreign key to Property
    public PurchaseDetailDto? PurchaseDetail { get; set; }
    public ICollection<InitialInvestmentDto> InitialInvestments { get; set; } = [];
    public RentDto? Rent { get; set; }
    public DepreciationDto? Depreciation { get; set; }
    public ReservesDto? Reserves { get; set; }
    public ForecastDto? Forecast { get; set; }
    public OperatingCostsDto? OperatingCosts { get; set; }
    public ICollection<LoanDto> Loans { get; set; } = [];
}