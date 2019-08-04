using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication8.Data.Migrations
{
    public partial class RenamedReminderToNoteAndChangedPropertyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Todos",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Reminders",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Todos",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Reminders",
                newName: "Note");
        }
    }
}
