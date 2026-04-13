using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class EtapaChecklistEntregaRepository : IEtapaChecklistEntregaRepository
{
    private readonly ApplicationDbContext _context;

    public EtapaChecklistEntregaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EtapaChecklistEntrega?> GetByIdAsync(Guid id)
    {
        return await _context.Set<EtapaChecklistEntrega>()
            .FirstOrDefaultAsync(etapaChecklistEntrega => etapaChecklistEntrega.Id == id);
    }

    public async Task<IEnumerable<EtapaChecklistEntrega>> GetByEntregaIdAsync(Guid entregaId)
    {
        return await _context.Set<EtapaChecklistEntrega>()
            .Where(etapaChecklistEntrega => etapaChecklistEntrega.EntregaId == entregaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<EtapaChecklistEntrega>> GetAllAsync()
    {
        return await _context.Set<EtapaChecklistEntrega>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(EtapaChecklistEntrega etapaChecklistEntrega)
    {
        await _context.Set<EtapaChecklistEntrega>().AddAsync(etapaChecklistEntrega);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EtapaChecklistEntrega etapaChecklistEntrega)
    {
        _context.Set<EtapaChecklistEntrega>().Update(etapaChecklistEntrega);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var etapaChecklistEntrega = await _context.Set<EtapaChecklistEntrega>().FirstOrDefaultAsync(x => x.Id == id);

        if (etapaChecklistEntrega is null)
        {
            return;
        }

        _context.Set<EtapaChecklistEntrega>().Remove(etapaChecklistEntrega);
        await _context.SaveChangesAsync();
    }
}
