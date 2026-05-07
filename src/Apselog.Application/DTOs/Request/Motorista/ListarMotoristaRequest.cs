using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request.Motorista;

public class ListarMotoristaRequest
{
    public Guid? Id { get; set; }
    public Guid? UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public MotoristaStatus? Status { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrdenarPor { get; set; }
    public bool Ascendente { get; set; } = true;
}
