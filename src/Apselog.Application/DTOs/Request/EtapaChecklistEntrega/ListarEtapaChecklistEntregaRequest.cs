using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request.EtapaChecklistEntrega;

public class ListarEtapaChecklistEntregaRequest
{
    public Guid? Id { get; set; }
    public Guid? EntregaId { get; set; }
    public Guid? EtapaChecklistModeloId { get; set; }
    public EtapaChecklistEntregaStatus? Status { get; set; }
    public bool? Concluida { get; set; }
    public Guid? ConcluidaPorUsuarioId { get; set; }
    public Guid? AssinaturaId { get; set; }
    public int? Ordem { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrdenarPor { get; set; }
    public bool Ascendente { get; set; } = true;
}
