using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model;

public class Rent
{
    public int Id { get; set; } // Primary key
    public decimal ColdRentPerSquareMeter { get; set; } // "Kaltmiete Wohnfläche pro qm"
    public decimal TotalColdRent { get; set; } // "Kaltmiete Wohnfläche gesamt"
    public decimal ParkingSpaces { get; set; } // "Stellplätze"
    public decimal Other { get; set; } // "Sonstiges"
    
    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}