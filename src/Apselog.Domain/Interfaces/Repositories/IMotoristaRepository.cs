using Apselog.Domain.Entities;
using Apselog.Domain.Enums;


namespace Apselog.Domain.Interfaces.Repositories;

public interface IMotoristaRepository
{
    // busca por id
    Task<Motorista?> GetByIdAsync(Guid id);
    // busca por status, ex: MotoristaStatus.Ativo
    Task<IEnumerable<Motorista>> GetByStatusAsync(MotoristaStatus status);
    // busca por email
    Task<Motorista?> GetByEmailAsync(string email);
    // lista todos
    Task<IEnumerable<Motorista>> GetAllAsync();
    // adiciona
    Task AddAsync(Motorista motorista);
    // atualiza por id
    Task UpdateAsync(Motorista motorista);
    // deleta por id
    Task DeleteAsync(Guid id);
}
