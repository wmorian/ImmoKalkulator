namespace kalkulator.net.Model.DTOs;

public class RentDto
{
    public decimal ColdRentPerSquareMeter { get; set; } // "Kaltmiete Wohnfläche pro qm" in German
    public decimal TotalColdRent { get; set; } // "Kaltmiete Wohnfläche gesamt" in German
    public decimal ParkingSpaces { get; set; } // "Stellplätze" in German
    public decimal Other { get; set; } // "Sonstiges" in German
}