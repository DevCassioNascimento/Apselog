using Apselog.Application.DTOs.Request.Assinatura;
using Apselog.Application.DTOs.Response.Assinatura;

namespace Apselog.Application.UseCases.Interfaces.Assinatura;

public interface IListarAssinaturaUseCase
{
    Task<IEnumerable<ListarAssinaturaResponse>> ExecutarAsync(ListarAssinaturaRequest request);
}
