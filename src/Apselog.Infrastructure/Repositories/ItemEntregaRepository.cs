using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class ItemEntregaRepository : IItemEntregaRepository
{
    private readonly ApplicationDbContext _context;

    public ItemEntregaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemEntrega?> GetByIdAsync(Guid id)
    {
        return await _context.Set<ItemEntrega>()
            .FirstOrDefaultAsync(itemEntrega => itemEntrega.Id == id);
    }

    public async Task<IEnumerable<ItemEntrega>> GetByEntregaIdAsync(Guid entregaId)
    {
        return await _context.Set<ItemEntrega>()
            .Where(itemEntrega => itemEntrega.EntregaId == entregaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ItemEntrega>> GetAllAsync()
    {
        return await _context.Set<ItemEntrega>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(ItemEntrega itemEntrega)
    {
        await _context.Set<ItemEntrega>().AddAsync(itemEntrega);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ItemEntrega itemEntrega)
    {
        _context.Set<ItemEntrega>().Update(itemEntrega);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var itemEntrega = await _context.Set<ItemEntrega>().FirstOrDefaultAsync(x => x.Id == id);

        if (itemEntrega is null)
        {
            return;
        }

        _context.Set<ItemEntrega>().Remove(itemEntrega);
        await _context.SaveChangesAsync();
    }
}
