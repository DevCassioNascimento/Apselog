using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class EventoEntregaRepository : IEventoEntregaRepository
{
    private readonly ApplicationDbContext _context;

    public EventoEntregaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EventoEntrega?> GetByIdAsync(Guid id)
    {
        return await _context.Set<EventoEntrega>()
            .FirstOrDefaultAsync(eventoEntrega => eventoEntrega.Id == id);
    }

    public async Task<IEnumerable<EventoEntrega>> GetByEntregaIdAsync(Guid entregaId)
    {
        return await _context.Set<EventoEntrega>()
            .Where(eventoEntrega => eventoEntrega.EntregaId == entregaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<EventoEntrega>> GetAllAsync()
    {
        return await _context.Set<EventoEntrega>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(EventoEntrega eventoEntrega)
    {
        await _context.Set<EventoEntrega>().AddAsync(eventoEntrega);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventoEntrega eventoEntrega)
    {
        _context.Set<EventoEntrega>().Update(eventoEntrega);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var eventoEntrega = await _context.Set<EventoEntrega>().FirstOrDefaultAsync(x => x.Id == id);

        if (eventoEntrega is null)
        {
            return;
        }

        _context.Set<EventoEntrega>().Remove(eventoEntrega);
        await _context.SaveChangesAsync();
    }
}
