using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;

namespace Apselog.Application.UseCases.Interfaces.Motorista;

public interface ICriarMotoristaUseCase
{
    Task<CriarMotoristaResponse> ExecutarAsync(CriarMotoristaRequest request);
}
