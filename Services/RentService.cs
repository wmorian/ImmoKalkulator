using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kalkulator.net.Model;

namespace kalkulator.net.Services;

public class RentService
{
    public RentCalcs GetRentCalcs(Calculation calculation)
    {
        var rent = calculation.Rent ?? throw new Exception($"{nameof(calculation.Rent)} cannot be null!");
        var operatingCosts = calculation.OperatingCosts ?? throw new Exception($"{nameof(calculation.OperatingCosts)} cannot be null!");

        double coldRent = rent.TotalColdRent + rent.ParkingSpaces + rent.Other;
        var sumOperationCostsAllocable = operatingCosts.HousingAllowanceAllocable
            + operatingCosts.PropertyTax
            + operatingCosts.OtherCosts.Where(c => c.IsAllocable).Sum(c => c.Cost);
        
        return new RentCalcs
        {
            ColdRent = coldRent,
            WarmRent = coldRent + sumOperationCostsAllocable
        };
    }
}

public class RentCalcs
{
    public double ColdRent { get; set; }
    public double WarmRent { get; set; }
}