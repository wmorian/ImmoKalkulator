namespace kalkulator.net.Model;

public class Calculation
{
    public int Id { get; set; } // Primary key
    public int PropertyId { get; set; } // Foreign key to Property
    public Property? Property { get; set; } // Navigation property back to Property

    // Navigation property for PurchaseDetail - assuming a 1:1 relationship
    public PurchaseDetail? PurchaseDetail { get; set; }

    // Navigation property for a list of InitialInvestments
    public ICollection<InitialInvestment> InitialInvestments { get; set; } = [];

    // Navigation property for Rent
    public Rent? Rent { get; set; }

    // Navigation property for Depreciation
    public Depreciation? Depreciation { get; set; }

    // Navigation property for Reserves
    public Reserves? Reserves { get; set; }

    // Navigation property for Forecast
    public AnnualForecast? Forecast { get; set; }

    // Navigation property for OperatingCosts
    public OperatingCosts? OperatingCosts { get; set; }

    // Navigation property for Loans
    public ICollection<Loan> Loans { get; set; } = [];
}