namespace kalkulator.net.Model.DTOs;

public class RentDto
{
    public decimal ColdRentPerSquareMeter { get; set; } // "Kaltmiete Wohnfläche pro qm"
    public decimal TotalColdRent { get; set; } // "Kaltmiete Wohnfläche gesamt"
    public decimal ParkingSpaces { get; set; } // "Stellplätze"
    public decimal Other { get; set; } // "Sonstiges"
}