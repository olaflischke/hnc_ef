using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingDayDal.Migrations
{
    public partial class TradingLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TradingLocation",
                table: "TradingDays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradingLocation",
                table: "TradingDays");
        }
    }
}
