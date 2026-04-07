using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IEnderecoRepository
{
    // busca por id
    Task<Endereco?> GetByIdAsync(Guid id);
    // lista todos
    Task<IEnumerable<Endereco>> GetAllAsync();
    // adiciona
    Task AddAsync(Endereco endereco);
    // atualiza por id
    Task UpdateAsync(Endereco endereco);
    // deleta por id
    Task DeleteAsync(Guid id);
}
