using Apselog.Application.DTOs.Request.ItemEntrega;
using Apselog.Application.DTOs.Response.ItemEntrega;
using Apselog.Application.UseCases.Interfaces.ItemEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.ItemEntrega;

public class ListarItemEntregaUseCase : IListarItemEntregaUseCase
{
    private readonly IItemEntregaRepository _itemEntregaRepository;

    public ListarItemEntregaUseCase(IItemEntregaRepository itemEntregaRepository)
    {
        _itemEntregaRepository = itemEntregaRepository;
    }

    public async Task<IEnumerable<ListarItemEntregaResponse>> ExecutarAsync(ListarItemEntregaRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.ItemEntrega> query = request.EntregaId.HasValue
            ? await _itemEntregaRepository.GetByEntregaIdAsync(request.EntregaId.Value)
            : await _itemEntregaRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(itemEntrega => itemEntrega.Id == request.Id.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Nome))
        {
            query = query.Where(itemEntrega =>
                itemEntrega.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Sku))
        {
            query = query.Where(itemEntrega =>
                (itemEntrega.Sku ?? string.Empty).Contains(request.Sku, StringComparison.OrdinalIgnoreCase));
        }

        if (request.Ordem.HasValue)
        {
            query = query.Where(itemEntrega => itemEntrega.Ordem == request.Ordem.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(itemEntrega => new ListarItemEntregaResponse
        {
            Id = itemEntrega.Id,
            EntregaId = itemEntrega.EntregaId,
            Nome = itemEntrega.Nome,
            Descricao = itemEntrega.Descricao,
            Sku = itemEntrega.Sku,
            Quantidade = itemEntrega.Quantidade,
            Unidade = itemEntrega.Unidade,
            ValorDeclarado = itemEntrega.ValorDeclarado,
            Ordem = itemEntrega.Ordem
        });
    }

    private static IEnumerable<Domain.Entities.ItemEntrega> AplicarOrdenacao(
        IEnumerable<Domain.Entities.ItemEntrega> itensEntrega,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? itensEntrega.OrderBy(itemEntrega => itemEntrega.Ordem)
                : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.Ordem);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.Id) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.Id),
            "entregaid" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.EntregaId) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.EntregaId),
            "nome" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.Nome) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.Nome),
            "sku" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.Sku) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.Sku),
            "quantidade" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.Quantidade) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.Quantidade),
            "unidadade" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.Unidade) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.Unidade),
            "valordeclarado" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.ValorDeclarado) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.ValorDeclarado),
            "ordem" => ascendente ? itensEntrega.OrderBy(itemEntrega => itemEntrega.Ordem) : itensEntrega.OrderByDescending(itemEntrega => itemEntrega.Ordem),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
