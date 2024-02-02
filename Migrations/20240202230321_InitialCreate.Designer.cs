﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kalkulator.net;

#nullable disable

namespace kalkulator.net.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240202230321_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("kalkulator.net.Model.AnnualForecast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CostIncreasePercentage")
                        .HasColumnType("REAL");

                    b.Property<double>("RentIncreasePercentage")
                        .HasColumnType("REAL");

                    b.Property<double>("ValueIncreasePercentage")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId")
                        .IsUnique();

                    b.ToTable("Forecasts");
                });

            modelBuilder.Entity("kalkulator.net.Model.Calculation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("Calculations");
                });

            modelBuilder.Entity("kalkulator.net.Model.Depreciation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("BuildingValuePercentageOfPurchasePrice")
                        .HasColumnType("REAL");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("DepreciationRate")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId")
                        .IsUnique();

                    b.ToTable("Depreciations");
                });

            modelBuilder.Entity("kalkulator.net.Model.InitialInvestment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Cost")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("TaxTreatment")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ValueIncrease")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId");

                    b.ToTable("InitialInvestments");
                });

            modelBuilder.Entity("kalkulator.net.Model.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("InitialRepaymentRate")
                        .HasColumnType("REAL");

                    b.Property<double>("InterestRate")
                        .HasColumnType("REAL");

                    b.Property<double>("LoanAmount")
                        .HasColumnType("REAL");

                    b.Property<double>("MonthlyPayment")
                        .HasColumnType("REAL");

                    b.Property<int>("YearOfFullRepayment")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("kalkulator.net.Model.OperatingCosts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("HomeownersAssociationReserve")
                        .HasColumnType("REAL");

                    b.Property<double>("HousingAllowanceAllocable")
                        .HasColumnType("REAL");

                    b.Property<double>("HousingAllowanceNonAllocable")
                        .HasColumnType("REAL");

                    b.Property<double>("PropertyTax")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId")
                        .IsUnique();

                    b.ToTable("OperatingCosts");
                });

            modelBuilder.Entity("kalkulator.net.Model.OtherOperatingCost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Cost")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsAllocable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("OperatingCostsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OperatingCostsId");

                    b.ToTable("OtherOperatingCost");
                });

            modelBuilder.Entity("kalkulator.net.Model.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<double>("LivingSpace")
                        .HasColumnType("REAL");

                    b.Property<int>("ParkingSpaces")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("kalkulator.net.Model.PurchaseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("BrokerCommissionPercentage")
                        .HasColumnType("REAL");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("LandRegistryFeePercentage")
                        .HasColumnType("REAL");

                    b.Property<double>("NotaryFeePercentage")
                        .HasColumnType("REAL");

                    b.Property<double>("OtherCostsPercentage")
                        .HasColumnType("REAL");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("REAL");

                    b.Property<double>("TransferTaxPercentage")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId")
                        .IsUnique();

                    b.ToTable("PcurchaseDetails");
                });

            modelBuilder.Entity("kalkulator.net.Model.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ColdRentPerSquareMeter")
                        .HasColumnType("REAL");

                    b.Property<double>("Other")
                        .HasColumnType("REAL");

                    b.Property<double>("ParkingSpaces")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalColdRent")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId")
                        .IsUnique();

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("kalkulator.net.Model.Reserves", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CalculatedRentLossPercentage")
                        .HasColumnType("REAL");

                    b.Property<int>("CalculationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MaintenanceReservePerSquareMeterPerAnnum")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId")
                        .IsUnique();

                    b.ToTable("Reserves");
                });

            modelBuilder.Entity("kalkulator.net.Model.AnnualForecast", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithOne("Forecast")
                        .HasForeignKey("kalkulator.net.Model.AnnualForecast", "CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.Calculation", b =>
                {
                    b.HasOne("kalkulator.net.Model.Property", "Property")
                        .WithMany("Calculations")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("kalkulator.net.Model.Depreciation", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithOne("Depreciation")
                        .HasForeignKey("kalkulator.net.Model.Depreciation", "CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.InitialInvestment", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithMany("InitialInvestments")
                        .HasForeignKey("CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.Loan", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithMany("Loans")
                        .HasForeignKey("CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.OperatingCosts", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithOne("OperatingCosts")
                        .HasForeignKey("kalkulator.net.Model.OperatingCosts", "CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.OtherOperatingCost", b =>
                {
                    b.HasOne("kalkulator.net.Model.OperatingCosts", null)
                        .WithMany("OtherCosts")
                        .HasForeignKey("OperatingCostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("kalkulator.net.Model.PurchaseDetail", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithOne("PurchaseDetail")
                        .HasForeignKey("kalkulator.net.Model.PurchaseDetail", "CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.Rent", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithOne("Rent")
                        .HasForeignKey("kalkulator.net.Model.Rent", "CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.Reserves", b =>
                {
                    b.HasOne("kalkulator.net.Model.Calculation", "Calculation")
                        .WithOne("Reserves")
                        .HasForeignKey("kalkulator.net.Model.Reserves", "CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calculation");
                });

            modelBuilder.Entity("kalkulator.net.Model.Calculation", b =>
                {
                    b.Navigation("Depreciation");

                    b.Navigation("Forecast");

                    b.Navigation("InitialInvestments");

                    b.Navigation("Loans");

                    b.Navigation("OperatingCosts");

                    b.Navigation("PurchaseDetail");

                    b.Navigation("Rent");

                    b.Navigation("Reserves");
                });

            modelBuilder.Entity("kalkulator.net.Model.OperatingCosts", b =>
                {
                    b.Navigation("OtherCosts");
                });

            modelBuilder.Entity("kalkulator.net.Model.Property", b =>
                {
                    b.Navigation("Calculations");
                });
#pragma warning restore 612, 618
        }
    }
}
