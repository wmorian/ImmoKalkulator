namespace kalkulator.net.Model.DTOs;

public class PurchaseDetailDto
{
    public double PurchasePrice { get; set; } // "Kaufpreis"
    public double BrokerCommissionPercentage { get; set; } // "Makler Provision"
    public double NotaryFeePercentage { get; set; } // "Notar"
    public double LandRegistryFeePercentage { get; set; } // "Grundbuch Amt"
    public double TransferTaxPercentage { get; set; } // "Grunderwerbssteuer"
    public double OtherCostsPercentage { get; set; } // "Sonstige"
    public int Id { get; set; } // Primary key
    public int CalculationId { get; set; } // Foreign key to Calculation
}