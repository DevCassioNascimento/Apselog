using Apselog.Application.DTOs.Request.EtapaChecklistEntrega;
using Apselog.Application.DTOs.Response.EtapaChecklistEntrega;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistEntrega;

public class ExcluirEtapaChecklistEntregaUseCase : IExcluirEtapaChecklistEntregaUseCase
{
    private readonly IEtapaChecklistEntregaRepository _etapaChecklistEntregaRepository;

    public ExcluirEtapaChecklistEntregaUseCase(IEtapaChecklistEntregaRepository etapaChecklistEntregaRepository)
    {
        _etapaChecklistEntregaRepository = etapaChecklistEntregaRepository;
    }

    public async Task<ExcluirEtapaChecklistEntregaResponse> ExecutarAsync(ExcluirEtapaChecklistEntregaRequest request)
    {
        var etapaChecklistEntrega = await _etapaChecklistEntregaRepository.GetByIdAsync(request.Id);

        if (etapaChecklistEntrega is null)
        {
            throw new KeyNotFoundException("EtapaChecklistEntrega nao encontrada.");
        }

        await _etapaChecklistEntregaRepository.DeleteAsync(request.Id);

        return new ExcluirEtapaChecklistEntregaResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "EtapaChecklistEntrega excluida com sucesso."
        };
    }
}
