using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model;

public class AnnualForecast
{
    public int Id { get; set; } // Primary key
    public double CostIncreasePercentage { get; set; } // "Kostensteigerung p.a."
    public double RentIncreasePercentage { get; set; } // "Mietsteigerung p.a."
    public double ValueIncreasePercentage { get; set; } // "Wertsteigerung p.a."

    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}
