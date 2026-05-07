using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apselog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePrevisaoChegadaFromEntrega : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevisaoChegada",
                table: "Entregas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrevisaoChegada",
                table: "Entregas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
