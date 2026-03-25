using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;

namespace Apselog.Application.UseCases.Interfaces;

public interface ICriarUserUseCase
{
    Task<UserResponse> ExecutarAsync(CriarUserRequest request);
}
