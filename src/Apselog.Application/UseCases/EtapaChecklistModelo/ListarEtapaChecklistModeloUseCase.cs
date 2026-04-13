using Apselog.Application.DTOs.Request.EtapaChecklistModelo;
using Apselog.Application.DTOs.Response.EtapaChecklistModelo;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistModelo;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistModelo;

public class ListarEtapaChecklistModeloUseCase : IListarEtapaChecklistModeloUseCase
{
    private readonly IEtapaChecklistModeloRepository _etapaChecklistModeloRepository;

    public ListarEtapaChecklistModeloUseCase(IEtapaChecklistModeloRepository etapaChecklistModeloRepository)
    {
        _etapaChecklistModeloRepository = etapaChecklistModeloRepository;
    }

    public async Task<IEnumerable<ListarEtapaChecklistModeloResponse>> ExecutarAsync(ListarEtapaChecklistModeloRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.EtapaChecklistModelo> query = await _etapaChecklistModeloRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(etapaChecklistModelo => etapaChecklistModelo.Id == request.Id.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Codigo))
        {
            query = query.Where(etapaChecklistModelo =>
                etapaChecklistModelo.Codigo.Contains(request.Codigo, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Nome))
        {
            query = query.Where(etapaChecklistModelo =>
                etapaChecklistModelo.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase));
        }

        if (request.TipoAssinante.HasValue)
        {
            query = query.Where(etapaChecklistModelo => etapaChecklistModelo.TipoAssinante == request.TipoAssinante.Value);
        }

        if (request.Obrigatoria.HasValue)
        {
            query = query.Where(etapaChecklistModelo => etapaChecklistModelo.Obrigatoria == request.Obrigatoria.Value);
        }

        if (request.RequerAssinatura.HasValue)
        {
            query = query.Where(etapaChecklistModelo => etapaChecklistModelo.RequerAssinatura == request.RequerAssinatura.Value);
        }

        if (request.Ativo.HasValue)
        {
            query = query.Where(etapaChecklistModelo => etapaChecklistModelo.Ativo == request.Ativo.Value);
        }

        if (request.Ordem.HasValue)
        {
            query = query.Where(etapaChecklistModelo => etapaChecklistModelo.Ordem == request.Ordem.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(etapaChecklistModelo => new ListarEtapaChecklistModeloResponse
        {
            Id = etapaChecklistModelo.Id,
            Codigo = etapaChecklistModelo.Codigo,
            Nome = etapaChecklistModelo.Nome,
            Descricao = etapaChecklistModelo.Descricao,
            Ordem = etapaChecklistModelo.Ordem,
            Obrigatoria = etapaChecklistModelo.Obrigatoria,
            RequerAssinatura = etapaChecklistModelo.RequerAssinatura,
            TipoAssinante = etapaChecklistModelo.TipoAssinante,
            Ativo = etapaChecklistModelo.Ativo
        });
    }

    private static IEnumerable<Domain.Entities.EtapaChecklistModelo> AplicarOrdenacao(
        IEnumerable<Domain.Entities.EtapaChecklistModelo> etapasChecklistModelo,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.Ordem)
                : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.Ordem);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.Id) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.Id),
            "codigo" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.Codigo) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.Codigo),
            "nome" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.Nome) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.Nome),
            "ordem" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.Ordem) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.Ordem),
            "obrigatoria" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.Obrigatoria) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.Obrigatoria),
            "requerassinatura" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.RequerAssinatura) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.RequerAssinatura),
            "tipoassinante" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.TipoAssinante) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.TipoAssinante),
            "ativo" => ascendente ? etapasChecklistModelo.OrderBy(etapaChecklistModelo => etapaChecklistModelo.Ativo) : etapasChecklistModelo.OrderByDescending(etapaChecklistModelo => etapaChecklistModelo.Ativo),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
