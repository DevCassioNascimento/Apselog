using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Apselog.Infrastructure.Repositories;
using Apselog.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace Apselog.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<IEntregaRepository, EntregaRepository>();
        services.AddScoped<IEtapaChecklistEntregaRepository, EtapaChecklistEntregaRepository>();
        services.AddScoped<IEtapaChecklistModeloRepository, EtapaChecklistModeloRepository>();
        services.AddScoped<IEventoEntregaRepository, EventoEntregaRepository>();
        services.AddScoped<IItemEntregaRepository, ItemEntregaRepository>();
        services.AddScoped<IMotoristaRepository, MotoristaRepository>();
        services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();

        return services;
    }
}
