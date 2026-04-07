using Apselog.Domain.Entities;
using Apselog.Domain.Enums;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Apselog.Infrastructure.Repositories;

public class MotoristaRepository : IMotoristaRepository
{
    private readonly ApplicationDbContext _context;

    public MotoristaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Motorista?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Motorista>().FirstOrDefaultAsync(motorista => motorista.Id == id);
    }

    public async Task<Motorista?> GetByEmailAsync(string email)
    {
        return await _context.Set<Motorista>().FirstOrDefaultAsync(motorista => motorista.Email == email);
    }

    public async Task<IEnumerable<Motorista>> GetByStatusAsync(MotoristaStatus status)
    {
        return await _context.Set<Motorista>()
            .Where(motorista => motorista.Status == status)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Motorista>> GetAllAsync()
    {
        return await _context.Set<Motorista>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Motorista motorista)
    {
        await _context.Set<Motorista>().AddAsync(motorista);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Motorista motorista)
    {
        _context.Set<Motorista>().Update(motorista);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var motorista = await _context.Set<Motorista>().FirstOrDefaultAsync(x => x.Id == id);

        if (motorista is null)
        {
            return;
        }

        _context.Set<Motorista>().Remove(motorista);
        await _context.SaveChangesAsync();
    }
}
