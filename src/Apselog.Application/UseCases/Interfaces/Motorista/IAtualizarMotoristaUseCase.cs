using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;

namespace Apselog.Application.UseCases.Interfaces.Motorista;

public interface IAtualizarMotoristaUseCase
{
    Task<AtualizarMotoristaResponse> ExecutarAsync(AtualizarMotoristaRequest request);
}
