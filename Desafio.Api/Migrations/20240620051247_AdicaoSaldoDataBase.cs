using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoSaldoDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "Pessoas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Pessoas");
        }
    }
}
