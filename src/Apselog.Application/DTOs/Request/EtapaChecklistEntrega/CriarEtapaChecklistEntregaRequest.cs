using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request.EtapaChecklistEntrega;

public class CriarEtapaChecklistEntregaRequest
{
    public Guid EntregaId { get; set; }
    public Guid EtapaChecklistModeloId { get; set; }
    public EtapaChecklistEntregaStatus Status { get; set; } = EtapaChecklistEntregaStatus.Pendente;
    public bool Concluida { get; set; }
    public string? ConcluidaEm { get; set; }
    public Guid? ConcluidaPorUsuarioId { get; set; }
    public Guid? AssinaturaId { get; set; }
    public string? Observacoes { get; set; }
    public int Ordem { get; set; }
}
