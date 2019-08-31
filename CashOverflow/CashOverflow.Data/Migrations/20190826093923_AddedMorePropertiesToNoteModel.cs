using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication8.Data.Migrations
{
    public partial class AddedMorePropertiesToNoteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Notes",
                newName: "DateCreated");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSticky",
                table: "Notes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TextColor",
                table: "Notes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsSticky",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "TextColor",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Notes",
                newName: "Date");
        }
    }
}
