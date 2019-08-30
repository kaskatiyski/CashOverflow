using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication8.Data.Migrations
{
    public partial class UpdateNotesTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reminders",
                table: "Reminders");

            migrationBuilder.RenameTable(
                name: "Reminders",
                newName: "Notes");

            migrationBuilder.RenameIndex(
                name: "IX_Reminders_UserId",
                table: "Notes",
                newName: "IX_Notes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Reminders");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_UserId",
                table: "Reminders",
                newName: "IX_Reminders_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reminders",
                table: "Reminders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
