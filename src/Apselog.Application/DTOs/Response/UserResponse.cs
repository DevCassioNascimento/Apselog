using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Response;

public class UserResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Cargo { get; set; }
    public required string Instituicao { get; set; }
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
}
