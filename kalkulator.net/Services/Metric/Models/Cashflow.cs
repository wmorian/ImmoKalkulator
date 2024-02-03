namespace kalkulator.net.Services.Metric;

public class Cashflow
{
    public double WarmRent { get; set; }
    public double OperatingCosts { get; set; }
    public double Interest { get; set; }
    public double Repayment { get; set; }
    public double OperatingCashflow { get; set; }
    public Tax Tax { get; set; } = new();
    public double NetCashflow { get; set; }
}
