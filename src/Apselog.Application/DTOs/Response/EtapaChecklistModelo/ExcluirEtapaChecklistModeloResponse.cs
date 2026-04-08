namespace Apselog.Application.DTOs.Response.EtapaChecklistModelo;

public class ExcluirEtapaChecklistModeloResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
