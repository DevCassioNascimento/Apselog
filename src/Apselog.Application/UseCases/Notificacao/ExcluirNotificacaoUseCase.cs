using Apselog.Application.DTOs.Request.Notificacao;
using Apselog.Application.DTOs.Response.Notificacao;
using Apselog.Application.UseCases.Interfaces.Notificacao;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Notificacao;

public class ExcluirNotificacaoUseCase : IExcluirNotificacaoUseCase
{
    private readonly INotificacaoRepository _notificacaoRepository;

    public ExcluirNotificacaoUseCase(INotificacaoRepository notificacaoRepository)
    {
        _notificacaoRepository = notificacaoRepository;
    }

    public async Task<ExcluirNotificacaoResponse> ExecutarAsync(ExcluirNotificacaoRequest request)
    {
        var notificacao = await _notificacaoRepository.GetByIdAsync(request.Id);

        if (notificacao is null)
        {
            throw new KeyNotFoundException("Notificacao nao encontrada.");
        }

        await _notificacaoRepository.DeleteAsync(request.Id);

        return new ExcluirNotificacaoResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "Notificacao excluida com sucesso."
        };
    }
}
