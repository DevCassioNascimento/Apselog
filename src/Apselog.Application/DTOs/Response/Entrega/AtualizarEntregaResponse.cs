using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Response.Entrega;

public class AtualizarEntregaResponse
{
    public Guid Id { get; set; }
    public required string Codigo { get; set; }
    public string? Descricao { get; set; }
    public string? Observacoes { get; set; }
    public required string ClienteNome { get; set; }
    public required string ClienteTelefone { get; set; }
    public required string DataPedido { get; set; }
    public Guid? EnderecoId { get; set; }
    public Guid? MotoristaId { get; set; }
    public Guid? VeiculoId { get; set; }
    public EntregaStatus Status { get; set; }
}
