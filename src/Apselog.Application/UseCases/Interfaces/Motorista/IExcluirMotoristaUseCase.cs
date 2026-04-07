using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;

namespace Apselog.Application.UseCases.Interfaces.Motorista;

public interface IExcluirMotoristaUseCase
{
    Task<ExcluirMotoristaResponse> ExecutarAsync(ExcluirMotoristaRequest request);
}
