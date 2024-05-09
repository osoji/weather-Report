using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherReport.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    WeatherHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AvTempratureC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvTempratureF = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWindMPH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWindKPH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Humidity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeatherCondition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ForecastDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.WeatherHistoryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");
        }
    }
}
