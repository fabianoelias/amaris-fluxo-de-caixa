using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeCaixa.Data.Migrations
{
    /// <inheritdoc />
    public partial class Movimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaixaId",
                table: "Movimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_CaixaId",
                table: "Movimento",
                column: "CaixaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Caixa_CaixaId",
                table: "Movimento",
                column: "CaixaId",
                principalTable: "Caixa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Caixa_CaixaId",
                table: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Movimento_CaixaId",
                table: "Movimento");

            migrationBuilder.DropColumn(
                name: "CaixaId",
                table: "Movimento");
        }
    }
}
