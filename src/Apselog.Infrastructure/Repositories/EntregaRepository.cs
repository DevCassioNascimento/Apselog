using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class EntregaRepository : IEntregaRepository
{
    private readonly ApplicationDbContext _context;

    public EntregaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Entrega?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Entrega>()
            .Include(entrega => entrega.Endereco)
            .FirstOrDefaultAsync(entrega => entrega.Id == id);
    }

    public async Task<IEnumerable<Entrega>> GetAllAsync()
    {
        return await _context.Set<Entrega>()
            .AsNoTracking()
            .Include(entrega => entrega.Endereco)
            .ToListAsync();
    }

    public async Task AddAsync(Entrega entrega)
    {
        await _context.Set<Entrega>().AddAsync(entrega);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Entrega entrega)
    {
        _context.Set<Entrega>().Update(entrega);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entrega = await _context.Set<Entrega>().FirstOrDefaultAsync(x => x.Id == id);

        if (entrega is null)
        {
            return;
        }

        _context.Set<Entrega>().Remove(entrega);
        await _context.SaveChangesAsync();
    }
}
