using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request.Assinatura;

public class CriarAssinaturaRequest
{
    public Guid EntregaId { get; set; }
    public Guid? EtapaChecklistEntregaId { get; set; }
    public required string AssinadoPorNome { get; set; }
    public string? AssinadoPorDocumento { get; set; }
    public TipoAssinante AssinadoPorTipo { get; set; } = TipoAssinante.Destinatario;
    public string? ImagemBase64 { get; set; }
    public string? ArquivoUrl { get; set; }
    public string? IpOrigem { get; set; }
    public string? DeviceInfo { get; set; }
    public required string AssinadoEm { get; set; }
}
