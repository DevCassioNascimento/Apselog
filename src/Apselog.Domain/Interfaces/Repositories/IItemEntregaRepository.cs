using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IItemEntregaRepository
{
    // busca por id
    Task<ItemEntrega?> GetByIdAsync(Guid id);
    // lista por entrega
    Task<IEnumerable<ItemEntrega>> GetByEntregaIdAsync(Guid entregaId);
    // lista todos
    Task<IEnumerable<ItemEntrega>> GetAllAsync();
    // adiciona
    Task AddAsync(ItemEntrega itemEntrega);
    // atualiza por id
    Task UpdateAsync(ItemEntrega itemEntrega);
    // deleta por id
    Task DeleteAsync(Guid id);
}
