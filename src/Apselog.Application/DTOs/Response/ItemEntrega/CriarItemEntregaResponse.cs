namespace Apselog.Application.DTOs.Response.ItemEntrega;

public class CriarItemEntregaResponse
{
    public Guid Id { get; set; }
    public Guid EntregaId { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Sku { get; set; }
    public int Quantidade { get; set; }
    public string? Unidade { get; set; }
    public decimal? ValorDeclarado { get; set; }
    public int Ordem { get; set; }
}
