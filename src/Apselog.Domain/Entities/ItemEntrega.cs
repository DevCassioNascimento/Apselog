namespace Apselog.Domain.Entities;

public class ItemEntrega
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EntregaId { get; set; }
    public Entrega Entrega { get; set; } = null!;
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Sku { get; set; }
    public int Quantidade { get; set; }
    public string? Unidade { get; set; }
    public decimal? ValorDeclarado { get; set; }
    public int Ordem { get; set; }
}
