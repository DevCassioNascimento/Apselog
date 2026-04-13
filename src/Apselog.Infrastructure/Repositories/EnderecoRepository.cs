using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly ApplicationDbContext _context;

    public EnderecoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Endereco?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Endereco>().FirstOrDefaultAsync(endereco => endereco.Id == id);
    }

    public async Task<IEnumerable<Endereco>> GetAllAsync()
    {
        return await _context.Set<Endereco>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Endereco endereco)
    {
        await _context.Set<Endereco>().AddAsync(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Endereco endereco)
    {
        _context.Set<Endereco>().Update(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var endereco = await _context.Set<Endereco>().FirstOrDefaultAsync(x => x.Id == id);

        if (endereco is null)
        {
            return;
        }

        _context.Set<Endereco>().Remove(endereco);
        await _context.SaveChangesAsync();
    }
}
