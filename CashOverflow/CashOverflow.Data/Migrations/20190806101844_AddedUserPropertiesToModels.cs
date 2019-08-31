using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication8.Data.Migrations
{
    public partial class AddedUserPropertiesToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reminders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RecurringPayments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserId",
                table: "Todos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayments_UserId",
                table: "RecurringPayments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayments_AspNetUsers_UserId",
                table: "RecurringPayments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_UserId",
                table: "Todos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayments_AspNetUsers_UserId",
                table: "RecurringPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_UserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_RecurringPayments_UserId",
                table: "RecurringPayments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RecurringPayments");
        }
    }
}
