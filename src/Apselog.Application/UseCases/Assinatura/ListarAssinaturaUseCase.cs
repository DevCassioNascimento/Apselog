using Apselog.Application.DTOs.Request.Assinatura;
using Apselog.Application.DTOs.Response.Assinatura;
using Apselog.Application.UseCases.Interfaces.Assinatura;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Assinatura;

public class ListarAssinaturaUseCase : IListarAssinaturaUseCase
{
    private readonly IAssinaturaRepository _assinaturaRepository;

    public ListarAssinaturaUseCase(IAssinaturaRepository assinaturaRepository)
    {
        _assinaturaRepository = assinaturaRepository;
    }

    public async Task<IEnumerable<ListarAssinaturaResponse>> ExecutarAsync(ListarAssinaturaRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.Assinatura> query = await _assinaturaRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(assinatura => assinatura.Id == request.Id.Value);
        }

        if (request.EntregaId.HasValue)
        {
            query = query.Where(assinatura => assinatura.EntregaId == request.EntregaId.Value);
        }

        if (request.EtapaChecklistEntregaId.HasValue)
        {
            query = query.Where(assinatura => assinatura.EtapaChecklistEntregaId == request.EtapaChecklistEntregaId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.AssinadoPorNome))
        {
            query = query.Where(assinatura =>
                assinatura.AssinadoPorNome.Contains(request.AssinadoPorNome, StringComparison.OrdinalIgnoreCase));
        }

        if (request.AssinadoPorTipo.HasValue)
        {
            query = query.Where(assinatura => assinatura.AssinadoPorTipo == request.AssinadoPorTipo.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(assinatura => new ListarAssinaturaResponse
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
        });
    }

    private static IEnumerable<Domain.Entities.Assinatura> AplicarOrdenacao(
        IEnumerable<Domain.Entities.Assinatura> assinaturas,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? assinaturas.OrderBy(assinatura => assinatura.AssinadoPorNome)
                : assinaturas.OrderByDescending(assinatura => assinatura.AssinadoPorNome);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? assinaturas.OrderBy(assinatura => assinatura.Id) : assinaturas.OrderByDescending(assinatura => assinatura.Id),
            "entregaid" => ascendente ? assinaturas.OrderBy(assinatura => assinatura.EntregaId) : assinaturas.OrderByDescending(assinatura => assinatura.EntregaId),
            "etapachecklistentregaid" => ascendente ? assinaturas.OrderBy(assinatura => assinatura.EtapaChecklistEntregaId) : assinaturas.OrderByDescending(assinatura => assinatura.EtapaChecklistEntregaId),
            "assinadopornome" => ascendente ? assinaturas.OrderBy(assinatura => assinatura.AssinadoPorNome) : assinaturas.OrderByDescending(assinatura => assinatura.AssinadoPorNome),
            "assinadopordocumento" => ascendente ? assinaturas.OrderBy(assinatura => assinatura.AssinadoPorDocumento) : assinaturas.OrderByDescending(assinatura => assinatura.AssinadoPorDocumento),
            "assinadoportipo" => ascendente ? assinaturas.OrderBy(assinatura => assinatura.AssinadoPorTipo) : assinaturas.OrderByDescending(assinatura => assinatura.AssinadoPorTipo),
            "assinadoem" => ascendente ? assinaturas.OrderBy(assinatura => assinatura.AssinadoEm) : assinaturas.OrderByDescending(assinatura => assinatura.AssinadoEm),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
