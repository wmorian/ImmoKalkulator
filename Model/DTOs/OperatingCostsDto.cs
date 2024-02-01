namespace kalkulator.net.Model.DTOs;

public class OperatingCostsDto
{
    public decimal HousingAllowanceAllocable { get; set; } // "Hausgeld (umlagefähiger Teil)" in German
    public decimal PropertyTax { get; set; } // "Grundsteuer" in German
    public decimal HousingAllowanceNonAllocable { get; set; } // "Hausgeld (nicht umlagefähiger Teil)" in German
    public decimal HomeownersAssociationReserve { get; set; } // "WEG Rücklage" in German
    public ICollection<OtherOperatingCost> OtherCosts { get; set; } = [];
}

public class OtherOperatingCostDto
{
    public string? Name { get; set; } // The name of the cost
    public decimal Cost { get; set; } // The amount of the cost
    public int OperatingCostsId { get; set; } // Foreign key to OperatingCosts
    public bool IsAllocable { get; set; } // True if the cost is allocable, false otherwise
}