using Apselog.Application.DTOs.Request.ItemEntrega;
using Apselog.Application.DTOs.Response.ItemEntrega;
using Apselog.Application.UseCases.Interfaces.ItemEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.ItemEntrega;

public class AtualizarItemEntregaUseCase : IAtualizarItemEntregaUseCase
{
    private readonly IItemEntregaRepository _itemEntregaRepository;

    public AtualizarItemEntregaUseCase(IItemEntregaRepository itemEntregaRepository)
    {
        _itemEntregaRepository = itemEntregaRepository;
    }

    public async Task<AtualizarItemEntregaResponse> ExecutarAsync(AtualizarItemEntregaRequest request)
    {
        var itemEntrega = await _itemEntregaRepository.GetByIdAsync(request.Id);

        if (itemEntrega is null)
        {
            throw new KeyNotFoundException("ItemEntrega nao encontrado.");
        }

        ValidarRequest(request);

        itemEntrega.EntregaId = request.EntregaId;
        itemEntrega.Nome = request.Nome;
        itemEntrega.Descricao = request.Descricao;
        itemEntrega.Sku = request.Sku;
        itemEntrega.Quantidade = request.Quantidade;
        itemEntrega.Unidade = request.Unidade;
        itemEntrega.ValorDeclarado = request.ValorDeclarado;
        itemEntrega.Ordem = request.Ordem;

        await _itemEntregaRepository.UpdateAsync(itemEntrega);

        return new AtualizarItemEntregaResponse
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

    private static void ValidarRequest(AtualizarItemEntregaRequest request)
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
