using Apselog.Domain.Entities;
using Apselog.Domain.Enums;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly ApplicationDbContext _context;

    public VeiculoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Veiculo?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Veiculo>().FirstOrDefaultAsync(veiculo => veiculo.Id == id);
    }

    public async Task<Veiculo?> GetByPlacaAsync(string placa)
    {
        return await _context.Set<Veiculo>().FirstOrDefaultAsync(veiculo => veiculo.Placa == placa);
    }

    public async Task<IEnumerable<Veiculo>> GetByStatusAsync(VeiculoStatus status)
    {
        return await _context.Set<Veiculo>()
            .Where(veiculo => veiculo.Status == status)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Veiculo>> GetAllAsync()
    {
        return await _context.Set<Veiculo>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Veiculo veiculo)
    {
        await _context.Set<Veiculo>().AddAsync(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Veiculo veiculo)
    {
        _context.Set<Veiculo>().Update(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var veiculo = await _context.Set<Veiculo>().FirstOrDefaultAsync(x => x.Id == id);

        if (veiculo is null)
        {
            return;
        }

        _context.Set<Veiculo>().Remove(veiculo);
        await _context.SaveChangesAsync();
    }
}
