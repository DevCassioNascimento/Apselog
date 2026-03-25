using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Request;

public class CriarUserRequest
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public required string Cargo { get; set; }
    public required string Instituicao { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Ativo;
}
