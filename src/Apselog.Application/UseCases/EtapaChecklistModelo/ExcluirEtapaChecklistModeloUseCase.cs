using Apselog.Application.DTOs.Request.EtapaChecklistModelo;
using Apselog.Application.DTOs.Response.EtapaChecklistModelo;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistModelo;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistModelo;

public class ExcluirEtapaChecklistModeloUseCase : IExcluirEtapaChecklistModeloUseCase
{
    private readonly IEtapaChecklistModeloRepository _etapaChecklistModeloRepository;

    public ExcluirEtapaChecklistModeloUseCase(IEtapaChecklistModeloRepository etapaChecklistModeloRepository)
    {
        _etapaChecklistModeloRepository = etapaChecklistModeloRepository;
    }

    public async Task<ExcluirEtapaChecklistModeloResponse> ExecutarAsync(ExcluirEtapaChecklistModeloRequest request)
    {
        var etapaChecklistModelo = await _etapaChecklistModeloRepository.GetByIdAsync(request.Id);

        if (etapaChecklistModelo is null)
        {
            throw new KeyNotFoundException("EtapaChecklistModelo nao encontrada.");
        }

        await _etapaChecklistModeloRepository.DeleteAsync(request.Id);

        return new ExcluirEtapaChecklistModeloResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "EtapaChecklistModelo excluida com sucesso."
        };
    }
}
