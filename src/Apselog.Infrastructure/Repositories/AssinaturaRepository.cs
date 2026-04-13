using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class AssinaturaRepository : IAssinaturaRepository
{
    private readonly ApplicationDbContext _context;

    public AssinaturaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Assinatura?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Assinatura>()
            .FirstOrDefaultAsync(assinatura => assinatura.Id == id);
    }

    public async Task<IEnumerable<Assinatura>> GetByEntregaIdAsync(Guid entregaId)
    {
        return await _context.Set<Assinatura>()
            .Where(assinatura => assinatura.EntregaId == entregaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Assinatura>> GetAllAsync()
    {
        return await _context.Set<Assinatura>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Assinatura assinatura)
    {
        await _context.Set<Assinatura>().AddAsync(assinatura);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Assinatura assinatura)
    {
        _context.Set<Assinatura>().Update(assinatura);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var assinatura = await _context.Set<Assinatura>().FirstOrDefaultAsync(x => x.Id == id);

        if (assinatura is null)
        {
            return;
        }

        _context.Set<Assinatura>().Remove(assinatura);
        await _context.SaveChangesAsync();
    }
}
