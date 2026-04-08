namespace Apselog.Application.DTOs.Response.EventoEntrega;

public class ExcluirEventoEntregaResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
