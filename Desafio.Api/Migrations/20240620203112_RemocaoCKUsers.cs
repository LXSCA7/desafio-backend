using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoCKUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_UserType",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_UserType",
                table: "Users",
                sql: "'UserType' IN ('Cliente', 'Lojista')");
        }
    }
}
