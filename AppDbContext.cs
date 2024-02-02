using kalkulator.net.Model;
using Microsoft.EntityFrameworkCore;

namespace kalkulator.net;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<Calculation> Calculations { get; set; }
    public DbSet<Depreciation> Depreciations { get; set; }
    public DbSet<AnnualForecast> Forecasts { get; set; }
    public DbSet<InitialInvestment> InitialInvestments { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<OperatingCosts> OperatingCosts { get; set; }
    public DbSet<PurchaseDetail> PcurchaseDetails { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Reserves> Reserves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-many relationship between Property and Calculations
        modelBuilder.Entity<Property>()
            .HasMany(p => p.Calculations)
            .WithOne(c => c.Property)
            .HasForeignKey(c => c.PropertyId);

        // One-to-one relationship between Calculation and PurchaseDetail
        modelBuilder.Entity<Calculation>()
            .HasOne(c => c.PurchaseDetail)
            .WithOne(p => p.Calculation)
            .HasForeignKey<PurchaseDetail>(p => p.CalculationId);

        // One-to-many relationship between Calculation and InitialInvestments
        modelBuilder.Entity<Calculation>()
            .HasMany(c => c.InitialInvestments)
            .WithOne(i => i.Calculation)
            .HasForeignKey(i => i.CalculationId);

        // One-to-one relationship between Calculation and Rent
        modelBuilder.Entity<Calculation>()
            .HasOne(c => c.Rent)
            .WithOne(r => r.Calculation)
            .HasForeignKey<Rent>(r => r.CalculationId);

        // One-to-one relationship between Calculation and Depreciation
        modelBuilder.Entity<Calculation>()
            .HasOne(c => c.Depreciation)
            .WithOne(d => d.Calculation)
            .HasForeignKey<Depreciation>(d => d.CalculationId);

        // One-to-one relationship between Calculation and Reserves
        modelBuilder.Entity<Calculation>()
            .HasOne(c => c.Reserves)
            .WithOne(r => r.Calculation)
            .HasForeignKey<Reserves>(r => r.CalculationId);

        // One-to-one relationship between Calculation and Forecast
        modelBuilder.Entity<Calculation>()
            .HasOne(c => c.Forecast)
            .WithOne(f => f.Calculation)
            .HasForeignKey<AnnualForecast>(f => f.CalculationId);

        // One-to-one relationship between Calculation and OperatingCosts
        modelBuilder.Entity<Calculation>()
            .HasOne(c => c.OperatingCosts)
            .WithOne(o => o.Calculation)
            .HasForeignKey<OperatingCosts>(o => o.CalculationId);

        // One-to-many relationship for variable costs
        modelBuilder.Entity<OperatingCosts>()
            .HasMany(o => o.OtherCosts)
            .WithOne() // Assuming no navigation property back to OperatingCosts from VariableCost
            .HasForeignKey(vc => vc.OperatingCostsId)
            .OnDelete(DeleteBehavior.Cascade); // Ensure proper cascade delete behavior

        // One-to-many relationship between Calculation and Loan
        modelBuilder.Entity<Calculation>()
            .HasMany(c => c.Loans)
            .WithOne(l => l.Calculation)
            .HasForeignKey(l => l.CalculationId);
    }
}