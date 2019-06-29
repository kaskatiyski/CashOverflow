using Microsoft.EntityFrameworkCore.Migrations;

namespace CashOverflow.Web.Data.Migrations
{
    public partial class UpdatedTransactionModelWithRecipientField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transactions",
                newName: "Recipient");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Recipient",
                table: "Transactions",
                newName: "Description");
        }
    }
}
