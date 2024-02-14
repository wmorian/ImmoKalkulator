namespace kalkulator.net.Model.DTOs;

public class ReservesDto
{
    public double CalculatedRentLossPercentage { get; set; } // "Kalkulatorischer Mietausfall"
    public double MaintenanceReservePerSquareMeterPerAnnum { get; set; } // "Eigene Instandhalt.-Rückl. (pro m² p.a.)"
    public int Id { get; set; } // Primary key
    public int CalculationId { get; set; } // Foreign key to Calculation
}