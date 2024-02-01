using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kalkulator.net.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Abbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    LivingSpace = table.Column<double>(type: "REAL", nullable: false),
                    ParkingSpaces = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calculations_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Depreciations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepreciationRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuildingValuePercentageOfPurchasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depreciations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depreciations_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnnualCostIncrease = table.Column<decimal>(type: "TEXT", nullable: false),
                    AnnualRentIncrease = table.Column<decimal>(type: "TEXT", nullable: false),
                    AnnualValueIncrease = table.Column<decimal>(type: "TEXT", nullable: false),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecasts_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitialInvestments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxTreatment = table.Column<int>(type: "INTEGER", nullable: false),
                    ValueIncrease = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialInvestments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitialInvestments_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoanAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    InterestRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    InitialRepaymentRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    MonthlyPayment = table.Column<decimal>(type: "TEXT", nullable: false),
                    YearOfFullRepayment = table.Column<int>(type: "INTEGER", nullable: false),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperatingCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HousingAllowanceAllocable = table.Column<decimal>(type: "TEXT", nullable: false),
                    PropertyTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    HousingAllowanceNonAllocable = table.Column<decimal>(type: "TEXT", nullable: false),
                    HomeownersAssociationReserve = table.Column<decimal>(type: "TEXT", nullable: false),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatingCosts_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcurchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BrokerCommission = table.Column<decimal>(type: "TEXT", nullable: false),
                    NotaryFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    LandRegistryFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    RealEstateTransferTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    OtherCosts = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcurchaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcurchaseDetails_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ColdRentPerSquareMeter = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalColdRent = table.Column<decimal>(type: "TEXT", nullable: false),
                    ParkingSpaces = table.Column<decimal>(type: "TEXT", nullable: false),
                    Other = table.Column<decimal>(type: "TEXT", nullable: false),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalculatedRentLoss = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaintenanceReservePerSquareMeterPerAnnum = table.Column<decimal>(type: "TEXT", nullable: false),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserves_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherOperatingCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    OperatingCostsId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAllocable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherOperatingCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherOperatingCost_OperatingCosts_OperatingCostsId",
                        column: x => x.OperatingCostsId,
                        principalTable: "OperatingCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_PropertyId",
                table: "Calculations",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Depreciations_CalculationId",
                table: "Depreciations",
                column: "CalculationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_CalculationId",
                table: "Forecasts",
                column: "CalculationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InitialInvestments_CalculationId",
                table: "InitialInvestments",
                column: "CalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CalculationId",
                table: "Loans",
                column: "CalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingCosts_CalculationId",
                table: "OperatingCosts",
                column: "CalculationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtherOperatingCost_OperatingCostsId",
                table: "OtherOperatingCost",
                column: "OperatingCostsId");

            migrationBuilder.CreateIndex(
                name: "IX_PcurchaseDetails_CalculationId",
                table: "PcurchaseDetails",
                column: "CalculationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CalculationId",
                table: "Rents",
                column: "CalculationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_CalculationId",
                table: "Reserves",
                column: "CalculationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depreciations");

            migrationBuilder.DropTable(
                name: "Forecasts");

            migrationBuilder.DropTable(
                name: "InitialInvestments");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "OtherOperatingCost");

            migrationBuilder.DropTable(
                name: "PcurchaseDetails");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Reserves");

            migrationBuilder.DropTable(
                name: "OperatingCosts");

            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
