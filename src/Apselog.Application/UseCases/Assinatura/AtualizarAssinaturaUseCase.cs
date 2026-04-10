using Apselog.Application.DTOs.Request.Assinatura;
using Apselog.Application.DTOs.Response.Assinatura;
using Apselog.Application.UseCases.Interfaces.Assinatura;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Assinatura;

public class AtualizarAssinaturaUseCase : IAtualizarAssinaturaUseCase
{
    private readonly IAssinaturaRepository _assinaturaRepository;

    public AtualizarAssinaturaUseCase(IAssinaturaRepository assinaturaRepository)
    {
        _assinaturaRepository = assinaturaRepository;
    }

    public async Task<AtualizarAssinaturaResponse> ExecutarAsync(AtualizarAssinaturaRequest request)
    {
        var assinatura = await _assinaturaRepository.GetByIdAsync(request.Id);

        if (assinatura is null)
        {
            throw new KeyNotFoundException("Assinatura nao encontrada.");
        }

        ValidarRequest(request);

        assinatura.EntregaId = request.EntregaId;
        assinatura.EtapaChecklistEntregaId = request.EtapaChecklistEntregaId;
        assinatura.AssinadoPorNome = request.AssinadoPorNome;
        assinatura.AssinadoPorDocumento = request.AssinadoPorDocumento;
        assinatura.AssinadoPorTipo = request.AssinadoPorTipo;
        assinatura.ImagemBase64 = request.ImagemBase64;
        assinatura.ArquivoUrl = request.ArquivoUrl;
        assinatura.IpOrigem = request.IpOrigem;
        assinatura.DeviceInfo = request.DeviceInfo;
        assinatura.AssinadoEm = request.AssinadoEm;

        await _assinaturaRepository.UpdateAsync(assinatura);

        return new AtualizarAssinaturaResponse
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

    private static void ValidarRequest(AtualizarAssinaturaRequest request)
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
}
