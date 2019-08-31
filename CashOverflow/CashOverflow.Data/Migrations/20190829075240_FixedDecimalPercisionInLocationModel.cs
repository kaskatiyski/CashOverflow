using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication8.Data.Migrations
{
    public partial class FixedDecimalPercisionInLocationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Locations",
                type: "decimal(18,16)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Locations",
                type: "decimal(18,16)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,16)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,16)");
        }
    }
}
