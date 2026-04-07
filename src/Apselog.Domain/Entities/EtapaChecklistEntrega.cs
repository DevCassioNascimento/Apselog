namespace Apselog.Domain.Entities;

public class EtapaChecklistEntrega
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EntregaId { get; set; }
    public Entrega Entrega { get; set; } = null!;
    public Guid EtapaChecklistModeloId { get; set; }
    public EtapaChecklistModelo EtapaChecklistModelo { get; set; } = null!;
    public string Status { get; set; } = "PENDENTE";
    public bool Concluida { get; set; }
    public string? ConcluidaEm { get; set; }
    public Guid? ConcluidaPorUsuarioId { get; set; }
    public User? ConcluidaPorUsuario { get; set; }
    public Guid? AssinaturaId { get; set; }
    public Assinatura? Assinatura { get; set; }
    public string? Observacoes { get; set; }
    public int Ordem { get; set; }
}
