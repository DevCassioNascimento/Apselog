using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IAssinaturaRepository
{
    // busca por id
    Task<Assinatura?> GetByIdAsync(Guid id);
    // lista por entrega
    Task<IEnumerable<Assinatura>> GetByEntregaIdAsync(Guid entregaId);
    // lista todos
    Task<IEnumerable<Assinatura>> GetAllAsync();
    // adiciona
    Task AddAsync(Assinatura assinatura);
    // atualiza por id
    Task UpdateAsync(Assinatura assinatura);
    // deleta por id
    Task DeleteAsync(Guid id);
}
