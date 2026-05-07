using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apselog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixAssinaturaEtapaChecklistRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assinatura_Entregas_EntregaId",
                table: "Assinatura");

            migrationBuilder.DropForeignKey(
                name: "FK_Assinatura_EtapasChecklistEntrega_EtapaChecklistEntregaId1",
                table: "Assinatura");

            migrationBuilder.DropIndex(
                name: "IX_Assinatura_EtapaChecklistEntregaId1",
                table: "Assinatura");

            migrationBuilder.DropColumn(
                name: "EtapaChecklistEntregaId1",
                table: "Assinatura");

            migrationBuilder.CreateIndex(
                name: "IX_Entregas_EnderecoId",
                table: "Entregas",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_EtapaChecklistEntregaId",
                table: "Assinatura",
                column: "EtapaChecklistEntregaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assinatura_Entregas_EntregaId",
                table: "Assinatura",
                column: "EntregaId",
                principalTable: "Entregas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assinatura_EtapasChecklistEntrega_EtapaChecklistEntregaId",
                table: "Assinatura",
                column: "EtapaChecklistEntregaId",
                principalTable: "EtapasChecklistEntrega",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Entregas_Enderecos_EnderecoId",
                table: "Entregas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assinatura_Entregas_EntregaId",
                table: "Assinatura");

            migrationBuilder.DropForeignKey(
                name: "FK_Assinatura_EtapasChecklistEntrega_EtapaChecklistEntregaId",
                table: "Assinatura");

            migrationBuilder.DropForeignKey(
                name: "FK_Entregas_Enderecos_EnderecoId",
                table: "Entregas");

            migrationBuilder.DropIndex(
                name: "IX_Entregas_EnderecoId",
                table: "Entregas");

            migrationBuilder.DropIndex(
                name: "IX_Assinatura_EtapaChecklistEntregaId",
                table: "Assinatura");

            migrationBuilder.AddColumn<Guid>(
                name: "EtapaChecklistEntregaId1",
                table: "Assinatura",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_EtapaChecklistEntregaId1",
                table: "Assinatura",
                column: "EtapaChecklistEntregaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Assinatura_Entregas_EntregaId",
                table: "Assinatura",
                column: "EntregaId",
                principalTable: "Entregas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assinatura_EtapasChecklistEntrega_EtapaChecklistEntregaId1",
                table: "Assinatura",
                column: "EtapaChecklistEntregaId1",
                principalTable: "EtapasChecklistEntrega",
                principalColumn: "Id");
        }
    }
}
