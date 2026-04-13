using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IJwtProvider
{
    string GenerateToken(User user);
}
