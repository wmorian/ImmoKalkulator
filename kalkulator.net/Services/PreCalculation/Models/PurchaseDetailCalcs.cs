namespace kalkulator.net.Services.Precalculation;

// Kaufpreis und Kaufnebenkosten
public class PurchaseDetailCalcs
{
    public double PurchasePricePerQm { get; set; } // "Kaufpreis pro qm"
    public double BrokerCommission { get; set; } // "Makler Provision"
    public double NotaryFee { get; set; } // "Notar"
    public double LandRegistryFee { get; set; } // "Grundbuch Amt"
    public double TransferTax { get; set; } // "Grunderwerbssteuer"
    public double OtherCosts { get; set; } // "Sonstige"
    public double SumChargesInPecent { get; set; } // Kaufnebenkosten in %
    public double SumCharges { get; set; } // Kaufnebenkosten
}