using Apselog.Application.DTOs.Request.EventoEntrega;
using Apselog.Application.DTOs.Response.EventoEntrega;
using Apselog.Application.UseCases.Interfaces.EventoEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EventoEntrega;

public class AtualizarEventoEntregaUseCase : IAtualizarEventoEntregaUseCase
{
    private readonly IEventoEntregaRepository _eventoEntregaRepository;

    public AtualizarEventoEntregaUseCase(IEventoEntregaRepository eventoEntregaRepository)
    {
        _eventoEntregaRepository = eventoEntregaRepository;
    }

    public async Task<AtualizarEventoEntregaResponse> ExecutarAsync(AtualizarEventoEntregaRequest request)
    {
        var eventoEntrega = await _eventoEntregaRepository.GetByIdAsync(request.Id);

        if (eventoEntrega is null)
        {
            throw new KeyNotFoundException("EventoEntrega nao encontrado.");
        }

        ValidarRequest(request);

        eventoEntrega.EntregaId = request.EntregaId;
        eventoEntrega.TipoEvento = request.TipoEvento;
        eventoEntrega.Descricao = request.Descricao;
        eventoEntrega.UsuarioId = request.UsuarioId;
        eventoEntrega.EtapaChecklistEntregaId = request.EtapaChecklistEntregaId;
        eventoEntrega.DataEvento = request.DataEvento;
        eventoEntrega.MetadataJson = request.MetadataJson;

        await _eventoEntregaRepository.UpdateAsync(eventoEntrega);

        return new AtualizarEventoEntregaResponse
        {
            Id = eventoEntrega.Id,
            EntregaId = eventoEntrega.EntregaId,
            TipoEvento = eventoEntrega.TipoEvento,
            Descricao = eventoEntrega.Descricao,
            UsuarioId = eventoEntrega.UsuarioId,
            EtapaChecklistEntregaId = eventoEntrega.EtapaChecklistEntregaId,
            DataEvento = eventoEntrega.DataEvento,
            MetadataJson = eventoEntrega.MetadataJson
        };
    }

    private static void ValidarRequest(AtualizarEventoEntregaRequest request)
    {
        if (request.EntregaId == Guid.Empty)
        {
            throw new ArgumentException("A entrega e obrigatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.Descricao))
        {
            throw new ArgumentException("A descricao e obrigatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.DataEvento))
        {
            throw new ArgumentException("A data do evento e obrigatoria.");
        }
    }
}
