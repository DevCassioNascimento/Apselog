using Apselog.Domain.Enums;

namespace Apselog.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string SenhaHash { get; set; }
    public required string Cargo { get; set; }
    public required string Instituicao { get; set; }
    public UserRole Role { get; set; } = UserRole.Usuario;
    public UserStatus Status { get; set; } = UserStatus.Ativo;
}
