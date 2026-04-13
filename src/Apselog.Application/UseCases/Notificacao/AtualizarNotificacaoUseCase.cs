using Apselog.Application.DTOs.Request.Notificacao;
using Apselog.Application.DTOs.Response.Notificacao;
using Apselog.Application.UseCases.Interfaces.Notificacao;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Notificacao;

public class AtualizarNotificacaoUseCase : IAtualizarNotificacaoUseCase
{
    private readonly INotificacaoRepository _notificacaoRepository;

    public AtualizarNotificacaoUseCase(INotificacaoRepository notificacaoRepository)
    {
        _notificacaoRepository = notificacaoRepository;
    }

    public async Task<AtualizarNotificacaoResponse> ExecutarAsync(AtualizarNotificacaoRequest request)
    {
        var notificacao = await _notificacaoRepository.GetByIdAsync(request.Id);

        if (notificacao is null)
        {
            throw new KeyNotFoundException("Notificacao nao encontrada.");
        }

        ValidarRequest(request);

        notificacao.UsuarioId = request.UsuarioId;
        notificacao.EntregaId = request.EntregaId;
        notificacao.Tipo = request.Tipo;
        notificacao.Titulo = request.Titulo;
        notificacao.Mensagem = request.Mensagem;
        notificacao.Canal = request.Canal;
        notificacao.Status = request.Status;
        notificacao.LidaEm = request.LidaEm;
        notificacao.EnviadaEm = request.EnviadaEm;
        notificacao.PayloadJson = request.PayloadJson;

        await _notificacaoRepository.UpdateAsync(notificacao);

        return new AtualizarNotificacaoResponse
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

    private static void ValidarRequest(AtualizarNotificacaoRequest request)
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
