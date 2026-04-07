using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IEtapaChecklistModeloRepository
{
    // busca por id
    Task<EtapaChecklistModelo?> GetByIdAsync(Guid id);
    // busca por codigo
    Task<EtapaChecklistModelo?> GetByCodigoAsync(string codigo);
    // lista todos
    Task<IEnumerable<EtapaChecklistModelo>> GetAllAsync();
    // adiciona
    Task AddAsync(EtapaChecklistModelo etapaChecklistModelo);
    // atualiza por id
    Task UpdateAsync(EtapaChecklistModelo etapaChecklistModelo);
    // deleta por id
    Task DeleteAsync(Guid id);
}
