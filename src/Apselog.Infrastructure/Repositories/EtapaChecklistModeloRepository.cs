using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class EtapaChecklistModeloRepository : IEtapaChecklistModeloRepository
{
    private readonly ApplicationDbContext _context;

    public EtapaChecklistModeloRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EtapaChecklistModelo?> GetByIdAsync(Guid id)
    {
        return await _context.Set<EtapaChecklistModelo>()
            .FirstOrDefaultAsync(etapaChecklistModelo => etapaChecklistModelo.Id == id);
    }

    public async Task<EtapaChecklistModelo?> GetByCodigoAsync(string codigo)
    {
        return await _context.Set<EtapaChecklistModelo>()
            .FirstOrDefaultAsync(etapaChecklistModelo => etapaChecklistModelo.Codigo == codigo);
    }

    public async Task<IEnumerable<EtapaChecklistModelo>> GetAllAsync()
    {
        return await _context.Set<EtapaChecklistModelo>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(EtapaChecklistModelo etapaChecklistModelo)
    {
        await _context.Set<EtapaChecklistModelo>().AddAsync(etapaChecklistModelo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EtapaChecklistModelo etapaChecklistModelo)
    {
        _context.Set<EtapaChecklistModelo>().Update(etapaChecklistModelo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var etapaChecklistModelo = await _context.Set<EtapaChecklistModelo>().FirstOrDefaultAsync(x => x.Id == id);

        if (etapaChecklistModelo is null)
        {
            return;
        }

        _context.Set<EtapaChecklistModelo>().Remove(etapaChecklistModelo);
        await _context.SaveChangesAsync();
    }
}
