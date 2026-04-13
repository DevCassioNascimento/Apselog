using Apselog.Application.DTOs.Request.ItemEntrega;
using Apselog.Application.DTOs.Response.ItemEntrega;
using Apselog.Application.UseCases.Interfaces.ItemEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.ItemEntrega;

public class ExcluirItemEntregaUseCase : IExcluirItemEntregaUseCase
{
    private readonly IItemEntregaRepository _itemEntregaRepository;

    public ExcluirItemEntregaUseCase(IItemEntregaRepository itemEntregaRepository)
    {
        _itemEntregaRepository = itemEntregaRepository;
    }

    public async Task<ExcluirItemEntregaResponse> ExecutarAsync(ExcluirItemEntregaRequest request)
    {
        var itemEntrega = await _itemEntregaRepository.GetByIdAsync(request.Id);

        if (itemEntrega is null)
        {
            throw new KeyNotFoundException("ItemEntrega nao encontrado.");
        }

        await _itemEntregaRepository.DeleteAsync(request.Id);

        return new ExcluirItemEntregaResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "ItemEntrega excluido com sucesso."
        };
    }
}
