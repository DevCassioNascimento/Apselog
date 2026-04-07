namespace Apselog.Domain.Entities;

public class EventoEntrega
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EntregaId { get; set; }
    public Entrega Entrega { get; set; } = null!;
    public required string TipoEvento { get; set; }
    public required string Descricao { get; set; }
    public Guid? UsuarioId { get; set; }
    public User? Usuario { get; set; }
    public Guid? EtapaChecklistEntregaId { get; set; }
    public EtapaChecklistEntrega? EtapaChecklistEntrega { get; set; }
    public required string DataEvento { get; set; }
    public string? MetadataJson { get; set; }
}
