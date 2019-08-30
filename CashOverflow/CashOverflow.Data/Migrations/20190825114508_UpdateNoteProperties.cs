using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication8.Data.Migrations
{
    public partial class UpdateNoteProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alert",
                table: "Reminders");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reminders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Payments",
                table: "RecurringPayments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Payments",
                table: "RecurringPayments");

            migrationBuilder.AddColumn<bool>(
                name: "Alert",
                table: "Reminders",
                nullable: false,
                defaultValue: false);
        }
    }
}
