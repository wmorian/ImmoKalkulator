namespace kalkulator.net.Services;

// Bewirtschaftungskosten
public class OperatingCostsCalcs
{
    public double SumOperationCostsAllocable { get; set; } // umlagefähige Kosten
    public double SumOfOperationCostsNonAllocable { get; set; } // nicht umlagefähige Kosten
    public double SumOfHousingAllowance { get; set; } // Hausgeld, excluding WEG Ruecklage
    public double SumOfOperationCosts { get; set; }
}
