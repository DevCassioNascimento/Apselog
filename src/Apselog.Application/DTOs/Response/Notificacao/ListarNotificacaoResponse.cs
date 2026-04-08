using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Response.Notificacao;

public class ListarNotificacaoResponse
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid? EntregaId { get; set; }
    public NotificacaoTipo Tipo { get; set; }
    public required string Titulo { get; set; }
    public required string Mensagem { get; set; }
    public NotificacaoCanal Canal { get; set; }
    public NotificacaoStatus Status { get; set; }
    public string? LidaEm { get; set; }
    public string? EnviadaEm { get; set; }
    public string? PayloadJson { get; set; }
}
