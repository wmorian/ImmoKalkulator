namespace kalkulator.net.Services.Precalculation;

// Bewirtschaftungskosten
public class OperatingCostsCalcs
{
    public double SumOperationCostsAllocable { get; set; } // umlagefähige Kosten
    public double SumOfOperationCostsNonAllocable { get; set; } // nicht umlagefähige Kosten
    public double PersonalMaintenanceReserves { get; set; } // Eigene Instandhaltungsrücklage
    public double CalculatedRentLoss { get; set; } // Kalkulatorischer Mietausfall
    public double SumOfHousingAllowance { get; set; } // Hausgeld, excluding WEG Ruecklage
    public double SumOfOperationCosts { get; set; }
}
