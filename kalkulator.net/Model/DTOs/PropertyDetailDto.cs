using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model.DTOs
{
    public class PropertyDetailDto
    {
        public int Id { get; set; }
        public string? Street { get; set; } // "Strasse"
        public string? City { get; set; } // "City"
        public string? PostalCode { get; set; } // "PLZ"
        public DateTime PurchaseDate { get; set; } // "Kaufdatum"
        public string? Abbreviation { get; set; } // "Kürzel"
        public double LivingSpace { get; set; } // "Wohnfläche"
        public int ParkingSpaces { get; set; } // "Stellplätze"
        public ICollection<CalculationDto> Calculations { get; set; } = [];
    }
}