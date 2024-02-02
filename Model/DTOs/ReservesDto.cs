namespace kalkulator.net.Model.DTOs;

public class ReservesDto
{
    public decimal CalculatedRentLoss { get; set; } // "Kalkulatorischer Mietausfall"
    public decimal MaintenanceReservePerSquareMeterPerAnnum { get; set; } // "Eigene Instandhalt.-Rückl. (pro m² p.a.)"
}