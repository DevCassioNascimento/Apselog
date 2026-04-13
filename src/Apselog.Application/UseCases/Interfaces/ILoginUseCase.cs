using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;

namespace Apselog.Application.UseCases.Interfaces;

public interface ILoginUseCase
{
    Task<LoginResponse> ExecutarAsync(LoginRequest request);
}
