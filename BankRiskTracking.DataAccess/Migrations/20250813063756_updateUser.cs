using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRiskTracking.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerIdi",
                table: "RiskHistories");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TransactionHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RiskHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "TransactionHistories");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RiskHistories");

            migrationBuilder.AddColumn<int>(
                name: "CustomerIdi",
                table: "RiskHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
