using Microsoft.Extensions.DependencyInjection;
using Apselog.Application.UseCases.Assinatura;
using Apselog.Application.UseCases.Entrega;
using Apselog.Application.UseCases;
using Apselog.Application.UseCases.Motorista;
using Apselog.Application.UseCases.Veiculo;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Application.UseCases.Interfaces.Assinatura;
using Apselog.Application.UseCases.Interfaces.Entrega;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Application.UseCases.Interfaces.Veiculo;

namespace Apselog.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //User
        services.AddScoped<ICriarUserUseCase, CriarUserUseCase>();
        services.AddScoped<IAtualizarUserUseCase, AtualizarUserUseCase>();
        services.AddScoped<IDeletarUserUseCase, DeletarUserUseCase>();
        //Entrega
        services.AddScoped<ICriarEntregaUseCase, CriarEntregaUseCase>();
        services.AddScoped<IAtualizarEntregaUseCase, AtualizarEntregaUseCase>();
        services.AddScoped<IListarEntregaUseCase, ListarEntregaUseCase>();
        services.AddScoped<IExcluirEntregaUseCase, ExcluirEntregaUseCase>();
        // Assinatura
        services.AddScoped<ICriarAssinaturaUseCase, CriarAssinaturaUseCase>();
        services.AddScoped<IAtualizarAssinaturaUseCase, AtualizarAssinaturaUseCase>();
        services.AddScoped<IListarAssinaturaUseCase, ListarAssinaturaUseCase>();
        services.AddScoped<IExcluirAssinaturaUseCase, ExcluirAssinaturaUseCase>();
        // Motorista
        services.AddScoped<ICriarMotoristaUseCase, CriarMotoristaUseCase>();
        services.AddScoped<IAtualizarMotoristaUseCase, AtualizarMotoristaUseCase>();
        services.AddScoped<IListarMotoristaUseCase, ListarMotoristaUseCase>();
        services.AddScoped<IExcluirMotoristaUseCase, ExcluirMotoristaUseCase>();
        // Veiculo
        services.AddScoped<ICriarVeiculoUseCase, CriarVeiculoUseCase>();
        services.AddScoped<IAtualizarVeiculoUseCase, AtualizarVeiculoUseCase>();
        services.AddScoped<IListarVeiculoUseCase, ListarVeiculoUseCase>();
        services.AddScoped<IExcluirVeiculoUseCase, ExcluirVeiculoUseCase>();

        return services;
    }
}
