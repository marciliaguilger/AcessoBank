using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Transfer.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountOrigin = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    AccountDestination = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransferStatus = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    TransferStatusDetail = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");
        }
    }
}
