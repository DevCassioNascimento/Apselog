using Apselog.Application.DTOs.Request.EtapaChecklistModelo;
using Apselog.Application.DTOs.Response.EtapaChecklistModelo;
using Apselog.Application.UseCases.Interfaces.EtapaChecklistModelo;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.EtapaChecklistModelo;

public class CriarEtapaChecklistModeloUseCase : ICriarEtapaChecklistModeloUseCase
{
    private readonly IEtapaChecklistModeloRepository _etapaChecklistModeloRepository;

    public CriarEtapaChecklistModeloUseCase(IEtapaChecklistModeloRepository etapaChecklistModeloRepository)
    {
        _etapaChecklistModeloRepository = etapaChecklistModeloRepository;
    }

    public async Task<CriarEtapaChecklistModeloResponse> ExecutarAsync(CriarEtapaChecklistModeloRequest request)
    {
        ValidarRequest(request);

        var etapaChecklistModeloExistente = await _etapaChecklistModeloRepository.GetByCodigoAsync(request.Codigo);

        if (etapaChecklistModeloExistente is not null)
        {
            throw new ArgumentException("Ja existe uma etapa checklist modelo com este codigo.");
        }

        var etapaChecklistModelo = new Domain.Entities.EtapaChecklistModelo
        {
            Codigo = request.Codigo,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Ordem = request.Ordem,
            Obrigatoria = request.Obrigatoria,
            RequerAssinatura = request.RequerAssinatura,
            TipoAssinante = request.TipoAssinante,
            Ativo = request.Ativo
        };

        await _etapaChecklistModeloRepository.AddAsync(etapaChecklistModelo);

        return new CriarEtapaChecklistModeloResponse
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

    private static void ValidarRequest(CriarEtapaChecklistModeloRequest request)
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
