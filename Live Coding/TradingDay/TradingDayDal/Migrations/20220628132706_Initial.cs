﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingDayDal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradingDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    TradingDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_TradingDays_TradingDayId",
                        column: x => x.TradingDayId,
                        principalTable: "TradingDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_TradingDayId",
                table: "ExchangeRates",
                column: "TradingDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "TradingDays");
        }
    }
}
