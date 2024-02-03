using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Model;

// Kaufpreis und Kaufnebenkosten
public class PurchaseDetail
{
    public int Id { get; set; } // Primary key
    public int CalculationId { get; set; } // Foreign key to Calculation
    public Calculation? Calculation { get; set; } // Navigation property back to Calculation
    public double PurchasePrice { get; set; } // "Kaufpreis"
    public double BrokerCommissionPercentage { get; set; } // "Makler Provision"
    public double NotaryFeePercentage { get; set; } // "Notar"
    public double LandRegistryFeePercentage { get; set; } // "Grundbuch Amt"
    public double TransferTaxPercentage { get; set; } // "Grunderwerbssteuer"
    public double OtherCostsPercentage { get; set; } // "Sonstige"
}
