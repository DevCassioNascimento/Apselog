using Apselog.Domain.Entities;

namespace Apselog.Domain.Interfaces.Repositories;

public interface INotificacaoRepository
{
    // busca por id
    Task<Notificacao?> GetByIdAsync(Guid id);
    // lista por usuario
    Task<IEnumerable<Notificacao>> GetByUsuarioIdAsync(Guid usuarioId);
    // lista por entrega
    Task<IEnumerable<Notificacao>> GetByEntregaIdAsync(Guid entregaId);
    // lista todos
    Task<IEnumerable<Notificacao>> GetAllAsync();
    // adiciona
    Task AddAsync(Notificacao notificacao);
    // atualiza por id
    Task UpdateAsync(Notificacao notificacao);
    // deleta por id
    Task DeleteAsync(Guid id);
}
