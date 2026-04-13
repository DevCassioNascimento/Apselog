using Apselog.Application.DTOs.Request.EtapaChecklistModelo;
using Apselog.Application.DTOs.Response.EtapaChecklistModelo;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistModelo;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistModelo;

public class AtualizarEtapaChecklistModeloUseCase : IAtualizarEtapaChecklistModeloUseCase
{
    private readonly IEtapaChecklistModeloRepository _etapaChecklistModeloRepository;

    public AtualizarEtapaChecklistModeloUseCase(IEtapaChecklistModeloRepository etapaChecklistModeloRepository)
    {
        _etapaChecklistModeloRepository = etapaChecklistModeloRepository;
    }

    public async Task<AtualizarEtapaChecklistModeloResponse> ExecutarAsync(AtualizarEtapaChecklistModeloRequest request)
    {
        var etapaChecklistModelo = await _etapaChecklistModeloRepository.GetByIdAsync(request.Id);

        if (etapaChecklistModelo is null)
        {
            throw new KeyNotFoundException("EtapaChecklistModelo nao encontrada.");
        }

        ValidarRequest(request);

        var etapaChecklistModeloComMesmoCodigo = await _etapaChecklistModeloRepository.GetByCodigoAsync(request.Codigo);

        if (etapaChecklistModeloComMesmoCodigo is not null && etapaChecklistModeloComMesmoCodigo.Id != request.Id)
        {
            throw new ArgumentException("Ja existe uma etapa checklist modelo com este codigo.");
        }

        etapaChecklistModelo.Codigo = request.Codigo;
        etapaChecklistModelo.Nome = request.Nome;
        etapaChecklistModelo.Descricao = request.Descricao;
        etapaChecklistModelo.Ordem = request.Ordem;
        etapaChecklistModelo.Obrigatoria = request.Obrigatoria;
        etapaChecklistModelo.RequerAssinatura = request.RequerAssinatura;
        etapaChecklistModelo.TipoAssinante = request.TipoAssinante;
        etapaChecklistModelo.Ativo = request.Ativo;

        await _etapaChecklistModeloRepository.UpdateAsync(etapaChecklistModelo);

        return new AtualizarEtapaChecklistModeloResponse
        {
            Id = etapaChecklistModelo.Id,
            Codigo = etapaChecklistModelo.Codigo,
            Nome = etapaChecklistModelo.Nome,
            Descricao = etapaChecklistModelo.Descricao,
            Ordem = etapaChecklistModelo.Ordem,
            Obrigatoria = etapaChecklistModelo.Obrigatoria,
            RequerAssinatura = etapaChecklistModelo.RequerAssinatura,
            TipoAssinante = etapaChecklistModelo.TipoAssinante,
            Ativo = etapaChecklistModelo.Ativo
        };
    }

    private static void ValidarRequest(AtualizarEtapaChecklistModeloRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Codigo))
        {
            throw new ArgumentException("O codigo e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            throw new ArgumentException("O nome e obrigatorio.");
        }

        if (request.Ordem < 0)
        {
            throw new ArgumentException("A ordem nao pode ser negativa.");
        }
    }
}
