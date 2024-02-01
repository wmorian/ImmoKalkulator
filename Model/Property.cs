namespace kalkulator.net.Model;

public class Property
{
    public int Id { get; set; } // Primary key
    public string? Street { get; set; } // "Strasse" in German
    public string? City { get; set; } // "City" in German
    public string? PostalCode { get; set; } // "PLZ" in German
    public DateTime PurchaseDate { get; set; } // "Kaufdatum" in German
    public string? Abbreviation { get; set; } // "Kürzel" in German
    public double LivingSpace { get; set; } // "Wohnfläche" in German
    public int ParkingSpaces { get; set; } // "Stellplätze" in German

    public ICollection<Calculation> Calculations { get; set; } = [];
}