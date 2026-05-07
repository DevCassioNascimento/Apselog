using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apselog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260507133000_RemoveDestinatarioUsuarioFromEntrega")]
    public partial class RemoveDestinatarioUsuarioFromEntrega : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entregas_Users_DestinatarioUsuarioId",
                table: "Entregas");

            migrationBuilder.DropIndex(
                name: "IX_Entregas_DestinatarioUsuarioId",
                table: "Entregas");

            migrationBuilder.DropColumn(
                name: "DestinatarioUsuarioId",
                table: "Entregas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DestinatarioUsuarioId",
                table: "Entregas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entregas_DestinatarioUsuarioId",
                table: "Entregas",
                column: "DestinatarioUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entregas_Users_DestinatarioUsuarioId",
                table: "Entregas",
                column: "DestinatarioUsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
