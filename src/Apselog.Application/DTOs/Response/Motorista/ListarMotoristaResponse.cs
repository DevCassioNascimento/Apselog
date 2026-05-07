using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Response.Motorista;

public class ListarMotoristaResponse
{
    public Guid Id { get; set; }
    public Guid? UsuarioId { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public MotoristaStatus Status { get; set; }
}
