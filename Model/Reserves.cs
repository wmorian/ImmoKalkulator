using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model;

public class Reserves
{
    public int Id { get; set; } // Primary key
    public decimal CalculatedRentLoss { get; set; } // "Kalkulatorischer Mietausfall"
    public decimal MaintenanceReservePerSquareMeterPerAnnum { get; set; } // "Eigene Instandhalt.-Rückl. (pro m² p.a.)"

    public int CalculationId { get; set; } // Foreign key to Calculation
    // Navigation property back to Calculation
    public Calculation? Calculation { get; set; }
}