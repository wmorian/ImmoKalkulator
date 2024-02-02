namespace kalkulator.net.Model;

public class Property
{
    public int Id { get; set; } // Primary key
    public string? Street { get; set; } // "Strasse"
    public string? City { get; set; } // "City"
    public string? PostalCode { get; set; } // "PLZ"
    public DateTime PurchaseDate { get; set; } // "Kaufdatum"
    public string? Abbreviation { get; set; } // "Kürzel"
    public double LivingSpace { get; set; } // "Wohnfläche"
    public int ParkingSpaces { get; set; } // "Stellplätze"

    public ICollection<Calculation> Calculations { get; set; } = [];
}