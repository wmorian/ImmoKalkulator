using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model;

public class Rent
{
    public int Id { get; set; } // Primary key
    public double ColdRentPerSquareMeter { get; set; } // "Kaltmiete Wohnfläche pro qm"
    public double TotalColdRent { get; set; } // "Kaltmiete Wohnfläche gesamt"
    public double ParkingSpaces { get; set; } // "Stellplätze"
    public double Other { get; set; } // "Sonstiges"
    
    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}