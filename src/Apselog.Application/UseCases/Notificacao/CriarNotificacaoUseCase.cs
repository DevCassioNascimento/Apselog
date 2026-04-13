using Apselog.Application.DTOs.Request.Notificacao;
using Apselog.Application.DTOs.Response.Notificacao;
using Apselog.Application.UseCases.Interfaces.Notificacao;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Notificacao;

public class CriarNotificacaoUseCase : ICriarNotificacaoUseCase
{
    private readonly INotificacaoRepository _notificacaoRepository;

    public CriarNotificacaoUseCase(INotificacaoRepository notificacaoRepository)
    {
        _notificacaoRepository = notificacaoRepository;
    }

    public async Task<CriarNotificacaoResponse> ExecutarAsync(CriarNotificacaoRequest request)
    {
        ValidarRequest(request);

        var notificacao = new Domain.Entities.Notificacao
        {
            UsuarioId = request.UsuarioId,
            EntregaId = request.EntregaId,
            Tipo = request.Tipo,
            Titulo = request.Titulo,
            Mensagem = request.Mensagem,
            Canal = request.Canal,
            Status = request.Status,
            LidaEm = request.LidaEm,
            EnviadaEm = request.EnviadaEm,
            PayloadJson = request.PayloadJson
        };

        await _notificacaoRepository.AddAsync(notificacao);

        return new CriarNotificacaoResponse
        {
            Id = notificacao.Id,
            UsuarioId = notificacao.UsuarioId,
            EntregaId = notificacao.EntregaId,
            Tipo = notificacao.Tipo,
            Titulo = notificacao.Titulo,
            Mensagem = notificacao.Mensagem,
            Canal = notificacao.Canal,
            Status = notificacao.Status,
            LidaEm = notificacao.LidaEm,
            EnviadaEm = notificacao.EnviadaEm,
            PayloadJson = notificacao.PayloadJson
        };
    }

    private static void ValidarRequest(CriarNotificacaoRequest request)
    {
        if (request.UsuarioId == Guid.Empty)
        {
            throw new ArgumentException("O usuario e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Titulo))
        {
            throw new ArgumentException("O titulo e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Mensagem))
        {
            throw new ArgumentException("A mensagem e obrigatoria.");
        }
    }
}
