using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request.Assinatura;

public class ListarAssinaturaRequest
{
    public Guid? Id { get; set; }
    public Guid? EntregaId { get; set; }
    public Guid? EtapaChecklistEntregaId { get; set; }
    public string? AssinadoPorNome { get; set; }
    public TipoAssinante? AssinadoPorTipo { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrdenarPor { get; set; }
    public bool Ascendente { get; set; } = true;
}
