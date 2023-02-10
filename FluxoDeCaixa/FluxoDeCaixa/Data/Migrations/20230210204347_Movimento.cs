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
            migrationBuilder.AddColumn<DateTime>(
                name: "Cadastro",
                table: "Movimento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cadastro",
                table: "Movimento");
        }
    }
}
