using Microsoft.Extensions.DependencyInjection;
using Apselog.Application.UseCases.Assinatura;
using Apselog.Application.UseCases.Entrega;
using Apselog.Application.UseCases;
using Apselog.Application.UseCases.Motorista;
using Apselog.Application.UseCases.Veiculo;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Application.UseCases.Interfaces.Assinatura;
using Apselog.Application.UseCases.Interfaces.Entrega;
using Apselog.Application.UseCases.Interfaces.Endereco;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistEntrega;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistModelo;
using Apselog.Application.UseCases.Interfaces.EventoEntrega;
using Apselog.Application.UseCases.Interfaces.ItemEntrega;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Application.UseCases.Interfaces.Notificacao;
using Apselog.Application.UseCases.Interfaces.Veiculo;
using Apselog.Application.UseCases.Endereco;
using Apselog.Application.UseCases.EtapaChecklistEntrega;
using Apselog.Application.UseCases.EtapaChecklistModelo;
using Apselog.Application.UseCases.EventoEntrega;
using Apselog.Application.UseCases.ItemEntrega;
using Apselog.Application.UseCases.Notificacao;

namespace Apselog.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //User
        services.AddScoped<ICriarUserUseCase, CriarUserUseCase>();
        services.AddScoped<IAtualizarUserUseCase, AtualizarUserUseCase>();
        services.AddScoped<IDeletarUserUseCase, DeletarUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        //Entrega
        services.AddScoped<ICriarEntregaUseCase, CriarEntregaUseCase>();
        services.AddScoped<IAtualizarEntregaUseCase, AtualizarEntregaUseCase>();
        services.AddScoped<IListarEntregaUseCase, ListarEntregaUseCase>();
        services.AddScoped<IExcluirEntregaUseCase, ExcluirEntregaUseCase>();
        // Endereco
        services.AddScoped<ICriarEnderecoUseCase, CriarEnderecoUseCase>();
        services.AddScoped<IAtualizarEnderecoUseCase, AtualizarEnderecoUseCase>();
        services.AddScoped<IListarEnderecoUseCase, ListarEnderecoUseCase>();
        services.AddScoped<IExcluirEnderecoUseCase, ExcluirEnderecoUseCase>();
        // EtapaChecklistEntrega
        services.AddScoped<ICriarEtapaChecklistEntregaUseCase, CriarEtapaChecklistEntregaUseCase>();
        services.AddScoped<IAtualizarEtapaChecklistEntregaUseCase, AtualizarEtapaChecklistEntregaUseCase>();
        services.AddScoped<IListarEtapaChecklistEntregaUseCase, ListarEtapaChecklistEntregaUseCase>();
        services.AddScoped<IExcluirEtapaChecklistEntregaUseCase, ExcluirEtapaChecklistEntregaUseCase>();
        // EtapaChecklistModelo
        services.AddScoped<ICriarEtapaChecklistModeloUseCase, CriarEtapaChecklistModeloUseCase>();
        services.AddScoped<IAtualizarEtapaChecklistModeloUseCase, AtualizarEtapaChecklistModeloUseCase>();
        services.AddScoped<IListarEtapaChecklistModeloUseCase, ListarEtapaChecklistModeloUseCase>();
        services.AddScoped<IExcluirEtapaChecklistModeloUseCase, ExcluirEtapaChecklistModeloUseCase>();
        // EventoEntrega
        services.AddScoped<ICriarEventoEntregaUseCase, CriarEventoEntregaUseCase>();
        services.AddScoped<IAtualizarEventoEntregaUseCase, AtualizarEventoEntregaUseCase>();
        services.AddScoped<IListarEventoEntregaUseCase, ListarEventoEntregaUseCase>();
        services.AddScoped<IExcluirEventoEntregaUseCase, ExcluirEventoEntregaUseCase>();
        // ItemEntrega
        services.AddScoped<ICriarItemEntregaUseCase, CriarItemEntregaUseCase>();
        services.AddScoped<IAtualizarItemEntregaUseCase, AtualizarItemEntregaUseCase>();
        services.AddScoped<IListarItemEntregaUseCase, ListarItemEntregaUseCase>();
        services.AddScoped<IExcluirItemEntregaUseCase, ExcluirItemEntregaUseCase>();
        // Notificacao
        services.AddScoped<ICriarNotificacaoUseCase, CriarNotificacaoUseCase>();
        services.AddScoped<IAtualizarNotificacaoUseCase, AtualizarNotificacaoUseCase>();
        services.AddScoped<IListarNotificacaoUseCase, ListarNotificacaoUseCase>();
        services.AddScoped<IExcluirNotificacaoUseCase, ExcluirNotificacaoUseCase>();
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
