using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apselog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EtapasChecklistModelo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Obrigatoria = table.Column<bool>(type: "bit", nullable: false),
                    RequerAssinatura = table.Column<bool>(type: "bit", nullable: false),
                    TipoAssinante = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapasChecklistModelo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motoristas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoristas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Instituicao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MotoristaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_Motoristas_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motoristas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entregas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ClienteNome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClienteTelefone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DataPedido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataPrevista = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrevisaoChegada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DataEntrega = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnderecoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MotoristaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VeiculoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DestinatarioUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entregas_Motoristas_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motoristas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Entregas_Users_DestinatarioUsuarioId",
                        column: x => x.DestinatarioUsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Entregas_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItensEntrega",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntregaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Sku = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Unidade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ValorDeclarado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Ordem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensEntrega", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensEntrega_Entregas_EntregaId",
                        column: x => x.EntregaId,
                        principalTable: "Entregas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntregaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Canal = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LidaEm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnviadaEm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PayloadJson = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Entregas_EntregaId",
                        column: x => x.EntregaId,
                        principalTable: "Entregas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntregaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EtapaChecklistEntregaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EtapaChecklistEntregaId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssinadoPorNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssinadoPorDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssinadoPorTipo = table.Column<int>(type: "int", nullable: false),
                    ImagemBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArquivoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpOrigem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssinadoEm = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinatura_Entregas_EntregaId",
                        column: x => x.EntregaId,
                        principalTable: "Entregas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EtapasChecklistEntrega",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntregaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EtapaChecklistModeloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Concluida = table.Column<bool>(type: "bit", nullable: false),
                    ConcluidaEm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcluidaPorUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Ordem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapasChecklistEntrega", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapasChecklistEntrega_Assinatura_AssinaturaId",
                        column: x => x.AssinaturaId,
                        principalTable: "Assinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EtapasChecklistEntrega_Entregas_EntregaId",
                        column: x => x.EntregaId,
                        principalTable: "Entregas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EtapasChecklistEntrega_EtapasChecklistModelo_EtapaChecklistModeloId",
                        column: x => x.EtapaChecklistModeloId,
                        principalTable: "EtapasChecklistModelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EtapasChecklistEntrega_Users_ConcluidaPorUsuarioId",
                        column: x => x.ConcluidaPorUsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EventosEntrega",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntregaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoEvento = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EtapaChecklistEntregaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataEvento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MetadataJson = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosEntrega", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventosEntrega_Entregas_EntregaId",
                        column: x => x.EntregaId,
                        principalTable: "Entregas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventosEntrega_EtapasChecklistEntrega_EtapaChecklistEntregaId",
                        column: x => x.EtapaChecklistEntregaId,
                        principalTable: "EtapasChecklistEntrega",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EventosEntrega_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_EntregaId",
                table: "Assinatura",
                column: "EntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_EtapaChecklistEntregaId1",
                table: "Assinatura",
                column: "EtapaChecklistEntregaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Entregas_Codigo",
                table: "Entregas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entregas_DestinatarioUsuarioId",
                table: "Entregas",
                column: "DestinatarioUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Entregas_MotoristaId",
                table: "Entregas",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entregas_VeiculoId",
                table: "Entregas",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapasChecklistEntrega_AssinaturaId",
                table: "EtapasChecklistEntrega",
                column: "AssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapasChecklistEntrega_ConcluidaPorUsuarioId",
                table: "EtapasChecklistEntrega",
                column: "ConcluidaPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapasChecklistEntrega_EntregaId",
                table: "EtapasChecklistEntrega",
                column: "EntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapasChecklistEntrega_EtapaChecklistModeloId",
                table: "EtapasChecklistEntrega",
                column: "EtapaChecklistModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapasChecklistModelo_Codigo",
                table: "EtapasChecklistModelo",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventosEntrega_EntregaId",
                table: "EventosEntrega",
                column: "EntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosEntrega_EtapaChecklistEntregaId",
                table: "EventosEntrega",
                column: "EtapaChecklistEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosEntrega_UsuarioId",
                table: "EventosEntrega",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensEntrega_EntregaId",
                table: "ItensEntrega",
                column: "EntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_Email",
                table: "Motoristas",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_EntregaId",
                table: "Notificacoes",
                column: "EntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_UsuarioId",
                table: "Notificacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_MotoristaId",
                table: "Veiculos",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_Placa",
                table: "Veiculos",
                column: "Placa",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assinatura_EtapasChecklistEntrega_EtapaChecklistEntregaId1",
                table: "Assinatura",
                column: "EtapaChecklistEntregaId1",
                principalTable: "EtapasChecklistEntrega",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assinatura_Entregas_EntregaId",
                table: "Assinatura");

            migrationBuilder.DropForeignKey(
                name: "FK_EtapasChecklistEntrega_Entregas_EntregaId",
                table: "EtapasChecklistEntrega");

            migrationBuilder.DropForeignKey(
                name: "FK_Assinatura_EtapasChecklistEntrega_EtapaChecklistEntregaId1",
                table: "Assinatura");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "EventosEntrega");

            migrationBuilder.DropTable(
                name: "ItensEntrega");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Entregas");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Motoristas");

            migrationBuilder.DropTable(
                name: "EtapasChecklistEntrega");

            migrationBuilder.DropTable(
                name: "Assinatura");

            migrationBuilder.DropTable(
                name: "EtapasChecklistModelo");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
