namespace kalkulator.net.Model.DTOs;

public class RentDto
{
    public double ColdRentPerSquareMeter { get; set; } // "Kaltmiete Wohnfläche pro qm"
    public double TotalColdRent { get; set; } // "Kaltmiete Wohnfläche gesamt"
    public double ParkingSpaces { get; set; } // "Stellplätze"
    public double Other { get; set; } // "Sonstiges"
}