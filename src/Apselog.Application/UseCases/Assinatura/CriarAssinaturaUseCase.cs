using Apselog.Application.DTOs.Request.Assinatura;
using Apselog.Application.DTOs.Response.Assinatura;
using Apselog.Application.UseCases.Interfaces.Assinatura;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Assinatura;

public class CriarAssinaturaUseCase : ICriarAssinaturaUseCase
{
    private readonly IAssinaturaRepository _assinaturaRepository;

    public CriarAssinaturaUseCase(IAssinaturaRepository assinaturaRepository)
    {
        _assinaturaRepository = assinaturaRepository;
    }

    public async Task<CriarAssinaturaResponse> ExecutarAsync(CriarAssinaturaRequest request)
    {
        ValidarRequest(request);

        var assinatura = new Domain.Entities.Assinatura
        {
            EntregaId = request.EntregaId,
            EtapaChecklistEntregaId = request.EtapaChecklistEntregaId,
            AssinadoPorNome = request.AssinadoPorNome,
            AssinadoPorDocumento = request.AssinadoPorDocumento,
            AssinadoPorTipo = request.AssinadoPorTipo,
            ImagemBase64 = request.ImagemBase64,
            ArquivoUrl = request.ArquivoUrl,
            IpOrigem = request.IpOrigem,
            DeviceInfo = request.DeviceInfo,
            AssinadoEm = request.AssinadoEm
        };

        await _assinaturaRepository.AddAsync(assinatura);

        return MapearResponse(assinatura);
    }

    private static void ValidarRequest(CriarAssinaturaRequest request)
    {
        if (request.EntregaId == Guid.Empty)
        {
            throw new ArgumentException("A entrega da assinatura e obrigatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.AssinadoPorNome))
        {
            throw new ArgumentException("O nome do assinante e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.AssinadoEm))
        {
            throw new ArgumentException("A data da assinatura e obrigatoria.");
        }
    }

    private static CriarAssinaturaResponse MapearResponse(Domain.Entities.Assinatura assinatura)
    {
        return new CriarAssinaturaResponse
        {
            Id = assinatura.Id,
            EntregaId = assinatura.EntregaId,
            EtapaChecklistEntregaId = assinatura.EtapaChecklistEntregaId,
            AssinadoPorNome = assinatura.AssinadoPorNome,
            AssinadoPorDocumento = assinatura.AssinadoPorDocumento,
            AssinadoPorTipo = assinatura.AssinadoPorTipo,
            ImagemBase64 = assinatura.ImagemBase64,
            ArquivoUrl = assinatura.ArquivoUrl,
            IpOrigem = assinatura.IpOrigem,
            DeviceInfo = assinatura.DeviceInfo,
            AssinadoEm = assinatura.AssinadoEm
        };
    }
}
