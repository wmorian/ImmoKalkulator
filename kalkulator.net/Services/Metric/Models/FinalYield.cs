namespace kalkulator.net.Services.Metric;

public class FinalYield
{
    public int CountYear { get; set; } 
    public double AssetGrowth { get; set; } // Vermögenszuwachs p.a. 
    public double AssetGrowthWithoutAppreciation { get; set; } // ohne Wertsteigerung
    public double AnnualReturn { get; set; } // Eigenkapitalrendite p.a.
    public double AnnualReturnWithoutAppreciation { get; set; } // ohne Wertsteigerung
}