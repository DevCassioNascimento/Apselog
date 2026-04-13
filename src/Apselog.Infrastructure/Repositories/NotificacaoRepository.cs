using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class NotificacaoRepository : INotificacaoRepository
{
    private readonly ApplicationDbContext _context;

    public NotificacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Notificacao?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Notificacao>()
            .FirstOrDefaultAsync(notificacao => notificacao.Id == id);
    }

    public async Task<IEnumerable<Notificacao>> GetByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Set<Notificacao>()
            .Where(notificacao => notificacao.UsuarioId == usuarioId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Notificacao>> GetByEntregaIdAsync(Guid entregaId)
    {
        return await _context.Set<Notificacao>()
            .Where(notificacao => notificacao.EntregaId == entregaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Notificacao>> GetAllAsync()
    {
        return await _context.Set<Notificacao>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Notificacao notificacao)
    {
        await _context.Set<Notificacao>().AddAsync(notificacao);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Notificacao notificacao)
    {
        _context.Set<Notificacao>().Update(notificacao);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var notificacao = await _context.Set<Notificacao>().FirstOrDefaultAsync(x => x.Id == id);

        if (notificacao is null)
        {
            return;
        }

        _context.Set<Notificacao>().Remove(notificacao);
        await _context.SaveChangesAsync();
    }
}
