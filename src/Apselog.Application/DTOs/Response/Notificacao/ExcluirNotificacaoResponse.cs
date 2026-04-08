namespace Apselog.Application.DTOs.Response.Notificacao;

public class ExcluirNotificacaoResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
