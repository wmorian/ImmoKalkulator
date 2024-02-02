namespace kalkulator.net.Model.DTOs;

public class PurchaseDetailDto
{
    public decimal PurchasePrice { get; set; } // "Kaufpreis"
    public decimal BrokerCommission { get; set; } // "Makler Provision"
    public decimal NotaryFee { get; set; } // "Notar"
    public decimal LandRegistryFee { get; set; } // "Grundbuch Amt"
    public decimal RealEstateTransferTax { get; set; } // "Grunderwerbssteuer"
    public decimal OtherCosts { get; set; } // "Sonstige"
}