namespace Apselog.Application.DTOs.Response.ItemEntrega;

public class ExcluirItemEntregaResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
