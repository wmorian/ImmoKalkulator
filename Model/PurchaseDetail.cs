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
    public decimal PurchasePrice { get; set; } // "Kaufpreis"
    public decimal BrokerCommission { get; set; } // "Makler Provision"
    public decimal NotaryFee { get; set; } // "Notar"
    public decimal LandRegistryFee { get; set; } // "Grundbuch Amt"
    public decimal RealEstateTransferTax { get; set; } // "Grunderwerbssteuer"
    public decimal OtherCosts { get; set; } // "Sonstige"
}
