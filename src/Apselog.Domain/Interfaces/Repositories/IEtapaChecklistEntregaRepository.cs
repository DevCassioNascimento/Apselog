using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IEtapaChecklistEntregaRepository
{
    // busca por id
    Task<EtapaChecklistEntrega?> GetByIdAsync(Guid id);
    // lista por entrega
    Task<IEnumerable<EtapaChecklistEntrega>> GetByEntregaIdAsync(Guid entregaId);
    // lista todos
    Task<IEnumerable<EtapaChecklistEntrega>> GetAllAsync();
    // adiciona
    Task AddAsync(EtapaChecklistEntrega etapaChecklistEntrega);
    // atualiza por id
    Task UpdateAsync(EtapaChecklistEntrega etapaChecklistEntrega);
    // deleta por id
    Task DeleteAsync(Guid id);
}
