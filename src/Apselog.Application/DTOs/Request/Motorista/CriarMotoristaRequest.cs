using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request.Motorista;

public class CriarMotoristaRequest
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public Guid? UsuarioId { get; set; }
    public MotoristaStatus Status { get; set; } = MotoristaStatus.Ativo;
}
