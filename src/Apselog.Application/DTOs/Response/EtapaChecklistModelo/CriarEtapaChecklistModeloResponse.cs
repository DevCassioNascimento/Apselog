using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Response.EtapaChecklistModelo;

public class CriarEtapaChecklistModeloResponse
{
    public Guid Id { get; set; }
    public required string Codigo { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public int Ordem { get; set; }
    public bool Obrigatoria { get; set; }
    public bool RequerAssinatura { get; set; }
    public TipoAssinante TipoAssinante { get; set; }
    public bool Ativo { get; set; }
}
