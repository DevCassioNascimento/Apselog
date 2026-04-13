using Apselog.Application.DTOs.Request.EventoEntrega;
using Apselog.Application.DTOs.Response.EventoEntrega;
using Apselog.Application.UseCases.Interfaces.EventoEntrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EventoEntrega;

public class ExcluirEventoEntregaUseCase : IExcluirEventoEntregaUseCase
{
    private readonly IEventoEntregaRepository _eventoEntregaRepository;

    public ExcluirEventoEntregaUseCase(IEventoEntregaRepository eventoEntregaRepository)
    {
        _eventoEntregaRepository = eventoEntregaRepository;
    }

    public async Task<ExcluirEventoEntregaResponse> ExecutarAsync(ExcluirEventoEntregaRequest request)
    {
        var eventoEntrega = await _eventoEntregaRepository.GetByIdAsync(request.Id);

        if (eventoEntrega is null)
        {
            throw new KeyNotFoundException("EventoEntrega nao encontrado.");
        }

        await _eventoEntregaRepository.DeleteAsync(request.Id);

        return new ExcluirEventoEntregaResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "EventoEntrega excluido com sucesso."
        };
    }
}
