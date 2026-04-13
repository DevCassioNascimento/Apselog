using Apselog.Application.DTOs.Request.EventoEntrega;
using Apselog.Application.DTOs.Response.EventoEntrega;
using Apselog.Application.UseCases.Interfaces.EventoEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EventoEntrega;

public class ListarEventoEntregaUseCase : IListarEventoEntregaUseCase
{
    private readonly IEventoEntregaRepository _eventoEntregaRepository;

    public ListarEventoEntregaUseCase(IEventoEntregaRepository eventoEntregaRepository)
    {
        _eventoEntregaRepository = eventoEntregaRepository;
    }

    public async Task<IEnumerable<ListarEventoEntregaResponse>> ExecutarAsync(ListarEventoEntregaRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.EventoEntrega> query = request.EntregaId.HasValue
            ? await _eventoEntregaRepository.GetByEntregaIdAsync(request.EntregaId.Value)
            : await _eventoEntregaRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(eventoEntrega => eventoEntrega.Id == request.Id.Value);
        }

        if (request.TipoEvento.HasValue)
        {
            query = query.Where(eventoEntrega => eventoEntrega.TipoEvento == request.TipoEvento.Value);
        }

        if (request.UsuarioId.HasValue)
        {
            query = query.Where(eventoEntrega => eventoEntrega.UsuarioId == request.UsuarioId.Value);
        }

        if (request.EtapaChecklistEntregaId.HasValue)
        {
            query = query.Where(eventoEntrega => eventoEntrega.EtapaChecklistEntregaId == request.EtapaChecklistEntregaId.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(eventoEntrega => new ListarEventoEntregaResponse
        {
            Id = eventoEntrega.Id,
            EntregaId = eventoEntrega.EntregaId,
            TipoEvento = eventoEntrega.TipoEvento,
            Descricao = eventoEntrega.Descricao,
            UsuarioId = eventoEntrega.UsuarioId,
            EtapaChecklistEntregaId = eventoEntrega.EtapaChecklistEntregaId,
            DataEvento = eventoEntrega.DataEvento,
            MetadataJson = eventoEntrega.MetadataJson
        });
    }

    private static IEnumerable<Domain.Entities.EventoEntrega> AplicarOrdenacao(
        IEnumerable<Domain.Entities.EventoEntrega> eventosEntrega,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? eventosEntrega.OrderBy(eventoEntrega => eventoEntrega.DataEvento)
                : eventosEntrega.OrderByDescending(eventoEntrega => eventoEntrega.DataEvento);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? eventosEntrega.OrderBy(eventoEntrega => eventoEntrega.Id) : eventosEntrega.OrderByDescending(eventoEntrega => eventoEntrega.Id),
            "entregaid" => ascendente ? eventosEntrega.OrderBy(eventoEntrega => eventoEntrega.EntregaId) : eventosEntrega.OrderByDescending(eventoEntrega => eventoEntrega.EntregaId),
            "tipoevento" => ascendente ? eventosEntrega.OrderBy(eventoEntrega => eventoEntrega.TipoEvento) : eventosEntrega.OrderByDescending(eventoEntrega => eventoEntrega.TipoEvento),
            "usuarioid" => ascendente ? eventosEntrega.OrderBy(eventoEntrega => eventoEntrega.UsuarioId) : eventosEntrega.OrderByDescending(eventoEntrega => eventoEntrega.UsuarioId),
            "etapachecklistentregaid" => ascendente ? eventosEntrega.OrderBy(eventoEntrega => eventoEntrega.EtapaChecklistEntregaId) : eventosEntrega.OrderByDescending(eventoEntrega => eventoEntrega.EtapaChecklistEntregaId),
            "dataevento" => ascendente ? eventosEntrega.OrderBy(eventoEntrega => eventoEntrega.DataEvento) : eventosEntrega.OrderByDescending(eventoEntrega => eventoEntrega.DataEvento),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
