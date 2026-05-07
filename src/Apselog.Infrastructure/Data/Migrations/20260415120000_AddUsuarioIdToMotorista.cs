using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apselog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdToMotorista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Motoristas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_UsuarioId",
                table: "Motoristas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motoristas_Users_UsuarioId",
                table: "Motoristas",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motoristas_Users_UsuarioId",
                table: "Motoristas");

            migrationBuilder.DropIndex(
                name: "IX_Motoristas_UsuarioId",
                table: "Motoristas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Motoristas");
        }
    }
}
