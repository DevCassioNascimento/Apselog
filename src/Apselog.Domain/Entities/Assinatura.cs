namespace Apselog.Domain.Entities;

public class Assinatura
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EntregaId { get; set; }
    public Entrega Entrega { get; set; } = null!;
    public Guid? EtapaChecklistEntregaId { get; set; }
    public EtapaChecklistEntrega? EtapaChecklistEntrega { get; set; }
    public required string AssinadoPorNome { get; set; }
    public string? AssinadoPorDocumento { get; set; }
    public required string AssinadoPorTipo { get; set; }
    public string? ImagemBase64 { get; set; }
    public string? ArquivoUrl { get; set; }
    public string? IpOrigem { get; set; }
    public string? DeviceInfo { get; set; }
    public required string AssinadoEm { get; set; }
}
