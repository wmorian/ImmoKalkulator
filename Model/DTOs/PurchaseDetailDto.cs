namespace kalkulator.net.Model.DTOs;

public class PurchaseDetailDto
{
    public decimal PurchasePrice { get; set; } // "Kaufpreis" in German
    public decimal BrokerCommission { get; set; } // "Makler Provision" in German
    public decimal NotaryFee { get; set; } // "Notar" in German
    public decimal LandRegistryFee { get; set; } // "Grundbuch Amt" in German
    public decimal RealEstateTransferTax { get; set; } // "Grunderwerbssteuer" in German
    public decimal OtherCosts { get; set; } // "Sonstige" in German
}