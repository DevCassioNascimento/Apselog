using Apselog.Application.DTOs.Request.EventoEntrega;
using Apselog.Application.DTOs.Response.EventoEntrega;
using Apselog.Application.UseCases.Interfaces.EventoEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EventoEntrega;

public class CriarEventoEntregaUseCase : ICriarEventoEntregaUseCase
{
    private readonly IEventoEntregaRepository _eventoEntregaRepository;

    public CriarEventoEntregaUseCase(IEventoEntregaRepository eventoEntregaRepository)
    {
        _eventoEntregaRepository = eventoEntregaRepository;
    }

    public async Task<CriarEventoEntregaResponse> ExecutarAsync(CriarEventoEntregaRequest request)
    {
        ValidarRequest(request);

        var eventoEntrega = new Domain.Entities.EventoEntrega
        {
            EntregaId = request.EntregaId,
            TipoEvento = request.TipoEvento,
            Descricao = request.Descricao,
            UsuarioId = request.UsuarioId,
            EtapaChecklistEntregaId = request.EtapaChecklistEntregaId,
            DataEvento = request.DataEvento,
            MetadataJson = request.MetadataJson
        };

        await _eventoEntregaRepository.AddAsync(eventoEntrega);

        return new CriarEventoEntregaResponse
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

    private static void ValidarRequest(CriarEventoEntregaRequest request)
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
