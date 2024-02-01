using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model;

public class PurchaseDetail
{
    public int Id { get; set; } // Primary key
    public int CalculationId { get; set; } // Foreign key to Calculation
    public Calculation? Calculation { get; set; } // Navigation property back to Calculation
    public decimal PurchasePrice { get; set; } // "Kaufpreis" in German
    public decimal BrokerCommission { get; set; } // "Makler Provision" in German
    public decimal NotaryFee { get; set; } // "Notar" in German
    public decimal LandRegistryFee { get; set; } // "Grundbuch Amt" in German
    public decimal RealEstateTransferTax { get; set; } // "Grunderwerbssteuer" in German
    public decimal OtherCosts { get; set; } // "Sonstige" in German
}
