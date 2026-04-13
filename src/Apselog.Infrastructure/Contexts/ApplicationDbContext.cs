using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();
    public DbSet<Entrega> Entregas => Set<Entrega>();
    public DbSet<EtapaChecklistEntrega> EtapasChecklistEntrega => Set<EtapaChecklistEntrega>();
    public DbSet<EtapaChecklistModelo> EtapasChecklistModelo => Set<EtapaChecklistModelo>();
    public DbSet<EventoEntrega> EventosEntrega => Set<EventoEntrega>();
    public DbSet<ItemEntrega> ItensEntrega => Set<ItemEntrega>();
    public DbSet<Motorista> Motoristas => Set<Motorista>();
    public DbSet<Notificacao> Notificacoes => Set<Notificacao>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
