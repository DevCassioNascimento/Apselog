using Apselog.Application.DTOs.Request.EtapaChecklistEntrega;
using Apselog.Application.DTOs.Response.EtapaChecklistEntrega;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistEntrega;

public class AtualizarEtapaChecklistEntregaUseCase : IAtualizarEtapaChecklistEntregaUseCase
{
    private readonly IEtapaChecklistEntregaRepository _etapaChecklistEntregaRepository;

    public AtualizarEtapaChecklistEntregaUseCase(IEtapaChecklistEntregaRepository etapaChecklistEntregaRepository)
    {
        _etapaChecklistEntregaRepository = etapaChecklistEntregaRepository;
    }

    public async Task<AtualizarEtapaChecklistEntregaResponse> ExecutarAsync(AtualizarEtapaChecklistEntregaRequest request)
    {
        var etapaChecklistEntrega = await _etapaChecklistEntregaRepository.GetByIdAsync(request.Id);

        if (etapaChecklistEntrega is null)
        {
            throw new KeyNotFoundException("EtapaChecklistEntrega nao encontrada.");
        }

        ValidarRequest(request);

        etapaChecklistEntrega.EntregaId = request.EntregaId;
        etapaChecklistEntrega.EtapaChecklistModeloId = request.EtapaChecklistModeloId;
        etapaChecklistEntrega.Status = request.Status;
        etapaChecklistEntrega.Concluida = request.Concluida;
        etapaChecklistEntrega.ConcluidaEm = request.ConcluidaEm;
        etapaChecklistEntrega.ConcluidaPorUsuarioId = request.ConcluidaPorUsuarioId;
        etapaChecklistEntrega.AssinaturaId = request.AssinaturaId;
        etapaChecklistEntrega.Observacoes = request.Observacoes;
        etapaChecklistEntrega.Ordem = request.Ordem;

        await _etapaChecklistEntregaRepository.UpdateAsync(etapaChecklistEntrega);

        return new AtualizarEtapaChecklistEntregaResponse
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

    private static void ValidarRequest(AtualizarEtapaChecklistEntregaRequest request)
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
