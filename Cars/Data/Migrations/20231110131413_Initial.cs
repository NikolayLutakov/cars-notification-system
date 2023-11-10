using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    TelegramChatId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BotKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Make = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Model = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    YearOfManifacturing = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationPlates = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CivilInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    InsuranceCompany = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivilInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CivilInsurances_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GearingChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    NextChangeMileage = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearingChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GearingChanges_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OilChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    NextChangeMileage = table.Column<int>(type: "integer", nullable: false),
                    OilMake = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    OilType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OilChanges_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalInspections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalInspections_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TollTaxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TollTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TollTaxes_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InshurancePremiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfPayment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    InsuranceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InshurancePremiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InshurancePremiums_CivilInsurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "CivilInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RegistrationPlates",
                table: "Cars",
                column: "RegistrationPlates",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CivilInsurances_CarId",
                table: "CivilInsurances",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_GearingChanges_CarId",
                table: "GearingChanges",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_InshurancePremiums_InsuranceId",
                table: "InshurancePremiums",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_OilChanges_CarId",
                table: "OilChanges",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalInspections_CarId",
                table: "TechnicalInspections",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TollTaxes_CarId",
                table: "TollTaxes",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearingChanges");

            migrationBuilder.DropTable(
                name: "InshurancePremiums");

            migrationBuilder.DropTable(
                name: "OilChanges");

            migrationBuilder.DropTable(
                name: "TechnicalInspections");

            migrationBuilder.DropTable(
                name: "TelegramBots");

            migrationBuilder.DropTable(
                name: "TollTaxes");

            migrationBuilder.DropTable(
                name: "CivilInsurances");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
