using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request.Motorista;

public class AtualizarMotoristaRequest
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public string? Senha { get; set; }
    public Guid? UsuarioId { get; set; }
    public MotoristaStatus Status { get; set; }
}
