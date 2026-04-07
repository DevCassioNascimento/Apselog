using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;

namespace Apselog.Application.UseCases.Interfaces.Motorista;

public interface IListarMotoristaUseCase
{
    Task<IEnumerable<ListarMotoristaResponse>> ExecutarAsync(ListarMotoristaRequest request);
}
