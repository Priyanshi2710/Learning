using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Countryname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryCode);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateCode);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Country",
                        principalColumn: "CountryCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cityname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityCode);
                    table.ForeignKey(
                        name: "FK_City_State_StateCode",
                        column: x => x.StateCode,
                        principalTable: "State",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<int>(type: "int", nullable: false),
                    CityCode = table.Column<int>(type: "int", nullable: false),
                    StateCode = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpID);
                    table.ForeignKey(
                        name: "FK_Employees_City_CityCode",
                        column: x => x.CityCode,
                        principalTable: "City",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Country_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Country",
                        principalColumn: "CountryCode",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_State_StateCode",
                        column: x => x.StateCode,
                        principalTable: "State",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_StateCode",
                table: "City",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CityCode",
                table: "Employees",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryCode",
                table: "Employees",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StateCode",
                table: "Employees",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryCode",
                table: "State",
                column: "CountryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
