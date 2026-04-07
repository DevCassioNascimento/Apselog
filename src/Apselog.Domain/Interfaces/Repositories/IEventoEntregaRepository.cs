using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface IEventoEntregaRepository
{
    // busca por id
    Task<EventoEntrega?> GetByIdAsync(Guid id);
    // lista por entrega
    Task<IEnumerable<EventoEntrega>> GetByEntregaIdAsync(Guid entregaId);
    // lista todos
    Task<IEnumerable<EventoEntrega>> GetAllAsync();
    // adiciona
    Task AddAsync(EventoEntrega eventoEntrega);
    // atualiza por id
    Task UpdateAsync(EventoEntrega eventoEntrega);
    // deleta por id
    Task DeleteAsync(Guid id);
}
