namespace Apselog.Application.DTOs.Request.ItemEntrega;

public class CriarItemEntregaRequest
{
    public Guid EntregaId { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public int Quantidade { get; set; }
    public string? Unidade { get; set; }
    public int Ordem { get; set; }
}
