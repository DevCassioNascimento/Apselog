namespace Apselog.Domain.Entities;

public class Notificacao
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UsuarioId { get; set; }
    public User Usuario { get; set; } = null!;
    public Guid? EntregaId { get; set; }
    public Entrega? Entrega { get; set; }
    public required string Tipo { get; set; }
    public required string Titulo { get; set; }
    public required string Mensagem { get; set; }
    public string Canal { get; set; } = "IN_APP";
    public string Status { get; set; } = "PENDENTE";
    public string? LidaEm { get; set; }
    public string? EnviadaEm { get; set; }
    public string? PayloadJson { get; set; }
}
