using Microsoft.Extensions.DependencyInjection;
using Apselog.Application.UseCases;
using Apselog.Application.UseCases.Interfaces;

namespace Apselog.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICriarUserUseCase, CriarUserUseCase>();
        services.AddScoped<IAtualizarUserUseCase, AtualizarUserUseCase>();
        services.AddScoped<IDeletarUserUseCase, DeletarUserUseCase>();

        return services;
    }
}
