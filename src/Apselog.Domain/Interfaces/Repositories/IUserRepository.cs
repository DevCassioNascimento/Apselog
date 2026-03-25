using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    // busca por id
    Task<User?> GetByIdAsync(Guid id);
    // busca por email
    Task<User?> GetByEmailAsync(string email);
    // lista todos
    Task<IEnumerable<User>> GetAllAsync();
    // adiciona
    Task AddAsync(User user);
    // atualiza por id
    Task UpdateAsync(User user);
    // deleta por id 
    Task DeleteAsync(Guid id);
}
