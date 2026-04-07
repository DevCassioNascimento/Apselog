using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Infrastructure.Contexts;
using Apselog.Infrastructure.Repositories;
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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEntregaRepository, EntregaRepository>();
        services.AddScoped<IMotoristaRepository, MotoristaRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();

        return services;
    }
}
