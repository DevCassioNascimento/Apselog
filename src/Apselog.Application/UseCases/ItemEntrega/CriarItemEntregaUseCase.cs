using Apselog.Application.DTOs.Request.ItemEntrega;
using Apselog.Application.DTOs.Response.ItemEntrega;
using Apselog.Application.UseCases.Interfaces.ItemEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.ItemEntrega;

public class CriarItemEntregaUseCase : ICriarItemEntregaUseCase
{
    private readonly IItemEntregaRepository _itemEntregaRepository;

    public CriarItemEntregaUseCase(IItemEntregaRepository itemEntregaRepository)
    {
        _itemEntregaRepository = itemEntregaRepository;
    }

    public async Task<CriarItemEntregaResponse> ExecutarAsync(CriarItemEntregaRequest request)
    {
        ValidarRequest(request);

        var itemEntrega = new Domain.Entities.ItemEntrega
        {
            EntregaId = request.EntregaId,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Sku = request.Sku,
            Quantidade = request.Quantidade,
            Unidade = request.Unidade,
            ValorDeclarado = request.ValorDeclarado,
            Ordem = request.Ordem
        };

        await _itemEntregaRepository.AddAsync(itemEntrega);

        return new CriarItemEntregaResponse
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
        };
    }

    private static void ValidarRequest(CriarItemEntregaRequest request)
    {
        if (request.EntregaId == Guid.Empty)
        {
            throw new ArgumentException("A entrega e obrigatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            throw new ArgumentException("O nome do item e obrigatorio.");
        }

        if (request.Quantidade < 0)
        {
            throw new ArgumentException("A quantidade nao pode ser negativa.");
        }

        if (request.Ordem < 0)
        {
            throw new ArgumentException("A ordem nao pode ser negativa.");
        }
    }
}
