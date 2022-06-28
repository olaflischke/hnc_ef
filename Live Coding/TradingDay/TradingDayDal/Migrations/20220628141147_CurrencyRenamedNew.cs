using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingDayDal.Migrations
{
    public partial class CurrencyRenamedNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_TradingDays_TradingDayId",
                table: "ExchangeRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates");

            migrationBuilder.RenameTable(
                name: "ExchangeRates",
                newName: "Currencies");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_TradingDayId",
                table: "Currencies",
                newName: "IX_Currencies_TradingDayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_TradingDays_TradingDayId",
                table: "Currencies",
                column: "TradingDayId",
                principalTable: "TradingDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_TradingDays_TradingDayId",
                table: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "ExchangeRates");

            migrationBuilder.RenameIndex(
                name: "IX_Currencies_TradingDayId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_TradingDayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_TradingDays_TradingDayId",
                table: "ExchangeRates",
                column: "TradingDayId",
                principalTable: "TradingDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
