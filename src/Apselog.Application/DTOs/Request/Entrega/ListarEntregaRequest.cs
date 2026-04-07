using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request;

public class ListarEntregaRequest
{
    public Guid? Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public string? ClienteNome { get; set; }
    public Guid? MotoristaId { get; set; }
    public Guid? VeiculoId { get; set; }
    public Guid? DestinatarioUsuarioId { get; set; }
    public Guid? EnderecoId { get; set; }
    public EntregaStatus? Status { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrdenarPor { get; set; }
    public bool Ascendente { get; set; } = true;
}
