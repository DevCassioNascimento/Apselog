namespace Apselog.Application.DTOs.Response.EtapaChecklistEntrega;

public class ExcluirEtapaChecklistEntregaResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
