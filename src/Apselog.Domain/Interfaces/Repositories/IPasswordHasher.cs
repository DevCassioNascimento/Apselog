namespace Apselog.Domain.Interfaces.Repositories;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
