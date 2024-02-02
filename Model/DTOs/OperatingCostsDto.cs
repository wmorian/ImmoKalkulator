namespace kalkulator.net.Model.DTOs;

public class OperatingCostsDto
{
    public double HousingAllowanceAllocable { get; set; } // "Hausgeld (umlagefähiger Teil)"
    public double PropertyTax { get; set; } // "Grundsteuer"
    public double HousingAllowanceNonAllocable { get; set; } // "Hausgeld (nicht umlagefähiger Teil)"
    public double HomeownersAssociationReserve { get; set; } // "WEG Rücklage"
    public ICollection<OtherOperatingCost> OtherCosts { get; set; } = [];
}

public class OtherOperatingCostDto
{
    public string? Name { get; set; } // The name of the cost
    public double Cost { get; set; } // The amount of the cost
    public int OperatingCostsId { get; set; } // Foreign key to OperatingCosts
    public bool IsAllocable { get; set; } // True if the cost is allocable, false otherwise
}