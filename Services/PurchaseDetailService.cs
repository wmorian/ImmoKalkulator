using kalkulator.net.Model;

namespace kalkulator.net.Services;

// Kaufpreis und Kaufnebenkosten
public class PurchaseDetailService
{
    public PurchaseDetailCalcs GetPurchaseDetailCalcs(PurchaseDetail purchaseDetail, double livingSpace)
    {
        var sumChargesInPercent = purchaseDetail.BrokerCommissionPercentage
            + purchaseDetail.LandRegistryFeePercentage
            + purchaseDetail.NotaryFeePercentage
            + purchaseDetail.TransferTaxPercentage
            + purchaseDetail.OtherCostsPercentage;

        return new PurchaseDetailCalcs
        {
            PurchasePricePerQm = purchaseDetail.PurchasePrice / livingSpace,
            BrokerCommission = purchaseDetail.PurchasePrice * purchaseDetail.BrokerCommissionPercentage / 100,
            LandRegistryFee = purchaseDetail.PurchasePrice * purchaseDetail.LandRegistryFeePercentage / 100,
            NotaryFee = purchaseDetail.PurchasePrice * purchaseDetail.NotaryFeePercentage / 100,
            TransferTax = purchaseDetail.PurchasePrice * purchaseDetail.TransferTaxPercentage / 100,
            OtherCosts = purchaseDetail.PurchasePrice * purchaseDetail.OtherCostsPercentage / 100,
            SumChargesInPecent = sumChargesInPercent,
            SumCharges = purchaseDetail.PurchasePrice * sumChargesInPercent / 100
        };
    }
}

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