using Apselog.Application.DTOs.Request.EtapaChecklistEntrega;
using Apselog.Application.DTOs.Response.EtapaChecklistEntrega;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistEntrega;

public class CriarEtapaChecklistEntregaUseCase : ICriarEtapaChecklistEntregaUseCase
{
    private readonly IEtapaChecklistEntregaRepository _etapaChecklistEntregaRepository;

    public CriarEtapaChecklistEntregaUseCase(IEtapaChecklistEntregaRepository etapaChecklistEntregaRepository)
    {
        _etapaChecklistEntregaRepository = etapaChecklistEntregaRepository;
    }

    public async Task<CriarEtapaChecklistEntregaResponse> ExecutarAsync(CriarEtapaChecklistEntregaRequest request)
    {
        ValidarRequest(request);

        var etapaChecklistEntrega = new Domain.Entities.EtapaChecklistEntrega
        {
            EntregaId = request.EntregaId,
            EtapaChecklistModeloId = request.EtapaChecklistModeloId,
            Status = request.Status,
            Concluida = request.Concluida,
            ConcluidaEm = request.ConcluidaEm,
            ConcluidaPorUsuarioId = request.ConcluidaPorUsuarioId,
            AssinaturaId = request.AssinaturaId,
            Observacoes = request.Observacoes,
            Ordem = request.Ordem
        };

        await _etapaChecklistEntregaRepository.AddAsync(etapaChecklistEntrega);

        return new CriarEtapaChecklistEntregaResponse
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
        };
    }

    private static void ValidarRequest(CriarEtapaChecklistEntregaRequest request)
    {
        if (request.EntregaId == Guid.Empty)
        {
            throw new ArgumentException("A entrega e obrigatoria.");
        }

        if (request.EtapaChecklistModeloId == Guid.Empty)
        {
            throw new ArgumentException("A etapa do checklist modelo e obrigatoria.");
        }

        if (request.Ordem < 0)
        {
            throw new ArgumentException("A ordem nao pode ser negativa.");
        }
    }
}
