using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BizPilotBackEndProduction.Migrations
{
    /// <inheritdoc />
    public partial class invoice_model_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicesHeaders",
                columns: table => new
                {
                    InvId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    DisAmount = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    GrossInvAmount = table.Column<float>(type: "real", nullable: false),
                    isCancell = table.Column<bool>(type: "bit", nullable: false),
                    CancellUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesHeaders", x => x.InvId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    InvId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    NoOfUnits = table.Column<float>(type: "real", nullable: false),
                    InvPrice = table.Column<float>(type: "real", nullable: false),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoicehInvId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvId);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_InvoicesHeaders_InvoicehInvId",
                        column: x => x.InvoicehInvId,
                        principalTable: "InvoicesHeaders",
                        principalColumn: "InvId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoicehInvId",
                table: "InvoiceDetails",
                column: "InvoicehInvId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "InvoicesHeaders");
        }
    }
}
