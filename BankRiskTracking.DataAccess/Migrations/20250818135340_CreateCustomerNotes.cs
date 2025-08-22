using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRiskTracking.DataAccess.Migrations
{
    public partial class CreateCustomerNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // NOTE: Burada Users oluşturma YOK, rename YOK.

            migrationBuilder.CreateTable(
                name: "CustomerNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerNotes_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",   // DB'deki tablo adın "Customer" ise doğru.
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_CustomerId",
                table: "CustomerNotes",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CustomerNotes");
        }
    }
}