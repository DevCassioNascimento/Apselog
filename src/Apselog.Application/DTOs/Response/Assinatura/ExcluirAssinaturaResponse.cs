namespace Apselog.Application.DTOs.Response.Assinatura;

public class ExcluirAssinaturaResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
