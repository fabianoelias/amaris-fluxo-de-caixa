using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeCaixa.Data.Migrations
{
    /// <inheritdoc />
    public partial class Caixa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Caixa_StatusId",
                table: "Caixa",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_Status_StatusId",
                table: "Caixa",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_Status_StatusId",
                table: "Caixa");

            migrationBuilder.DropIndex(
                name: "IX_Caixa_StatusId",
                table: "Caixa");
        }
    }
}
