namespace Apselog.Application.DTOs.Request.ItemEntrega;

public class ListarItemEntregaRequest
{
    public Guid? Id { get; set; }
    public Guid? EntregaId { get; set; }
    public string? Nome { get; set; }
    public int? Ordem { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrdenarPor { get; set; }
    public bool Ascendente { get; set; } = true;
}
