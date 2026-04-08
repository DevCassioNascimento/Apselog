using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Response.Assinatura;

public class ListarAssinaturaResponse
{
    public Guid Id { get; set; }
    public Guid EntregaId { get; set; }
    public Guid? EtapaChecklistEntregaId { get; set; }
    public required string AssinadoPorNome { get; set; }
    public string? AssinadoPorDocumento { get; set; }
    public TipoAssinante AssinadoPorTipo { get; set; }
    public string? ImagemBase64 { get; set; }
    public string? ArquivoUrl { get; set; }
    public string? IpOrigem { get; set; }
    public string? DeviceInfo { get; set; }
    public required string AssinadoEm { get; set; }
}
