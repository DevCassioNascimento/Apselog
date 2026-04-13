using Apselog.Application.DTOs.Request.EtapaChecklistEntrega;
using Apselog.Application.DTOs.Response.EtapaChecklistEntrega;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistEntrega;

public class ListarEtapaChecklistEntregaUseCase : IListarEtapaChecklistEntregaUseCase
{
    private readonly IEtapaChecklistEntregaRepository _etapaChecklistEntregaRepository;

    public ListarEtapaChecklistEntregaUseCase(IEtapaChecklistEntregaRepository etapaChecklistEntregaRepository)
    {
        _etapaChecklistEntregaRepository = etapaChecklistEntregaRepository;
    }

    public async Task<IEnumerable<ListarEtapaChecklistEntregaResponse>> ExecutarAsync(ListarEtapaChecklistEntregaRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.EtapaChecklistEntrega> query = request.EntregaId.HasValue
            ? await _etapaChecklistEntregaRepository.GetByEntregaIdAsync(request.EntregaId.Value)
            : await _etapaChecklistEntregaRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(etapaChecklistEntrega => etapaChecklistEntrega.Id == request.Id.Value);
        }

        if (request.EtapaChecklistModeloId.HasValue)
        {
            query = query.Where(etapaChecklistEntrega => etapaChecklistEntrega.EtapaChecklistModeloId == request.EtapaChecklistModeloId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(etapaChecklistEntrega => etapaChecklistEntrega.Status == request.Status.Value);
        }

        if (request.Concluida.HasValue)
        {
            query = query.Where(etapaChecklistEntrega => etapaChecklistEntrega.Concluida == request.Concluida.Value);
        }

        if (request.ConcluidaPorUsuarioId.HasValue)
        {
            query = query.Where(etapaChecklistEntrega => etapaChecklistEntrega.ConcluidaPorUsuarioId == request.ConcluidaPorUsuarioId.Value);
        }

        if (request.AssinaturaId.HasValue)
        {
            query = query.Where(etapaChecklistEntrega => etapaChecklistEntrega.AssinaturaId == request.AssinaturaId.Value);
        }

        if (request.Ordem.HasValue)
        {
            query = query.Where(etapaChecklistEntrega => etapaChecklistEntrega.Ordem == request.Ordem.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(etapaChecklistEntrega => new ListarEtapaChecklistEntregaResponse
        {
            Id = etapaChecklistEntrega.Id,
            EntregaId = etapaChecklistEntrega.EntregaId,
            EtapaChecklistModeloId = etapaChecklistEntrega.EtapaChecklistModeloId,
            Status = etapaChecklistEntrega.Status,
            Concluida = etapaChecklistEntrega.Concluida,
            ConcluidaEm = etapaChecklistEntrega.ConcluidaEm,
            ConcluidaPorUsuarioId = etapaChecklistEntrega.ConcluidaPorUsuarioId,
            AssinaturaId = etapaChecklistEntrega.AssinaturaId,
            Observacoes = etapaChecklistEntrega.Observacoes,
            Ordem = etapaChecklistEntrega.Ordem
        });
    }

    private static IEnumerable<Domain.Entities.EtapaChecklistEntrega> AplicarOrdenacao(
        IEnumerable<Domain.Entities.EtapaChecklistEntrega> etapasChecklistEntrega,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.Ordem)
                : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.Ordem);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.Id) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.Id),
            "entregaid" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.EntregaId) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.EntregaId),
            "etapachecklistmodeloid" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.EtapaChecklistModeloId) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.EtapaChecklistModeloId),
            "status" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.Status) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.Status),
            "concluida" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.Concluida) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.Concluida),
            "concluidaem" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.ConcluidaEm) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.ConcluidaEm),
            "concluidaporusuarioid" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.ConcluidaPorUsuarioId) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.ConcluidaPorUsuarioId),
            "assinaturaid" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.AssinaturaId) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.AssinaturaId),
            "ordem" => ascendente ? etapasChecklistEntrega.OrderBy(etapaChecklistEntrega => etapaChecklistEntrega.Ordem) : etapasChecklistEntrega.OrderByDescending(etapaChecklistEntrega => etapaChecklistEntrega.Ordem),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
