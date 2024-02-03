using kalkulator.net.Model;

namespace kalkulator.net.Services;

// Bewirtschaftungskosten
public class OperatingCostsService
{
    public OperatingCostsCalcs GetOperatingCostsCalcs(Calculation calculation, double livingSpace)
    {
        var operatingCosts = calculation.OperatingCosts ?? throw new Exception($"{nameof(calculation.OperatingCosts)} cannot be null!");
        var rent = calculation.Rent ?? throw new Exception($"{nameof(calculation.Rent)} cannot be null!");
        var reserves = calculation.Reserves ?? throw new Exception($"{nameof(calculation.Reserves)} cannot be null!");

        var sumOperationCostsAllocable = operatingCosts.HousingAllowanceAllocable 
            + operatingCosts.PropertyTax 
            + operatingCosts.OtherCosts.Where(c => c.IsAllocable).Sum(c => c.Cost);
        
        var warmRent = rent.TotalColdRent + rent.ParkingSpaces + rent.Other + sumOperationCostsAllocable;
        var sumOfOperationCostsNonAllocable = operatingCosts.HousingAllowanceNonAllocable 
            + (livingSpace * reserves.MaintenanceReservePerSquareMeterPerAnnum / 12)
            + (reserves.CalculatedRentLossPercentage / 100 * warmRent)
            + operatingCosts.OtherCosts.Where(c => !c.IsAllocable).Sum(c => c.Cost);

        return new OperatingCostsCalcs
        {
            SumOperationCostsAllocable = sumOperationCostsAllocable,
            SumOfOperationCostsNonAllocable = sumOfOperationCostsNonAllocable,
            SumOfHousingAllowance = operatingCosts.HousingAllowanceAllocable + operatingCosts.HousingAllowanceNonAllocable,
            SumOfOperationCosts = sumOperationCostsAllocable + sumOfOperationCostsNonAllocable
        };
    }
}

public class OperatingCostsCalcs
{
    public double SumOperationCostsAllocable { get; set; } // umlagefähige Kosten
    public double SumOfOperationCostsNonAllocable { get; set; } // nicht umlagefähige Kosten
    public double SumOfHousingAllowance { get; set; } // Hausgeld, excluding WEG Ruecklage
    public double SumOfOperationCosts { get; set; }
}
