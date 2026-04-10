using Apselog.Application.DTOs.Request.Assinatura;
using Apselog.Application.DTOs.Response.Assinatura;
using Apselog.Application.UseCases.Interfaces.Assinatura;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Assinatura;

public class ExcluirAssinaturaUseCase : IExcluirAssinaturaUseCase
{
    private readonly IAssinaturaRepository _assinaturaRepository;

    public ExcluirAssinaturaUseCase(IAssinaturaRepository assinaturaRepository)
    {
        _assinaturaRepository = assinaturaRepository;
    }

    public async Task<ExcluirAssinaturaResponse> ExecutarAsync(ExcluirAssinaturaRequest request)
    {
        var assinatura = await _assinaturaRepository.GetByIdAsync(request.Id);

        if (assinatura is null)
        {
            throw new KeyNotFoundException("Assinatura nao encontrada.");
        }

        await _assinaturaRepository.DeleteAsync(request.Id);

        return new ExcluirAssinaturaResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "Assinatura excluida com sucesso."
        };
    }
}
