using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model;

public class Forecast
{
    public int Id { get; set; } // Primary key
    public decimal AnnualCostIncrease { get; set; } // "Kostensteigerung p.a." in German
    public decimal AnnualRentIncrease { get; set; } // "Mietsteigerung p.a." in German
    public decimal AnnualValueIncrease { get; set; } // "Wertsteigerung p.a." in German

    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}
