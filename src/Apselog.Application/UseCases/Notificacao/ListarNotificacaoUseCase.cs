using Apselog.Application.DTOs.Request.Notificacao;
using Apselog.Application.DTOs.Response.Notificacao;
using Apselog.Application.UseCases.Interfaces.Notificacao;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Notificacao;

public class ListarNotificacaoUseCase : IListarNotificacaoUseCase
{
    private readonly INotificacaoRepository _notificacaoRepository;

    public ListarNotificacaoUseCase(INotificacaoRepository notificacaoRepository)
    {
        _notificacaoRepository = notificacaoRepository;
    }

    public async Task<IEnumerable<ListarNotificacaoResponse>> ExecutarAsync(ListarNotificacaoRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.Notificacao> query = request.UsuarioId.HasValue
            ? await _notificacaoRepository.GetByUsuarioIdAsync(request.UsuarioId.Value)
            : await _notificacaoRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(notificacao => notificacao.Id == request.Id.Value);
        }

        if (request.EntregaId.HasValue)
        {
            query = query.Where(notificacao => notificacao.EntregaId == request.EntregaId.Value);
        }

        if (request.Tipo.HasValue)
        {
            query = query.Where(notificacao => notificacao.Tipo == request.Tipo.Value);
        }

        if (request.Canal.HasValue)
        {
            query = query.Where(notificacao => notificacao.Canal == request.Canal.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(notificacao => notificacao.Status == request.Status.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(notificacao => new ListarNotificacaoResponse
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
        });
    }

    private static IEnumerable<Domain.Entities.Notificacao> AplicarOrdenacao(
        IEnumerable<Domain.Entities.Notificacao> notificacoes,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? notificacoes.OrderBy(notificacao => notificacao.Titulo)
                : notificacoes.OrderByDescending(notificacao => notificacao.Titulo);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.Id) : notificacoes.OrderByDescending(notificacao => notificacao.Id),
            "usuarioid" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.UsuarioId) : notificacoes.OrderByDescending(notificacao => notificacao.UsuarioId),
            "entregaid" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.EntregaId) : notificacoes.OrderByDescending(notificacao => notificacao.EntregaId),
            "tipo" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.Tipo) : notificacoes.OrderByDescending(notificacao => notificacao.Tipo),
            "titulo" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.Titulo) : notificacoes.OrderByDescending(notificacao => notificacao.Titulo),
            "canal" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.Canal) : notificacoes.OrderByDescending(notificacao => notificacao.Canal),
            "status" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.Status) : notificacoes.OrderByDescending(notificacao => notificacao.Status),
            "lidaem" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.LidaEm) : notificacoes.OrderByDescending(notificacao => notificacao.LidaEm),
            "enviadaem" => ascendente ? notificacoes.OrderBy(notificacao => notificacao.EnviadaEm) : notificacoes.OrderByDescending(notificacao => notificacao.EnviadaEm),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
