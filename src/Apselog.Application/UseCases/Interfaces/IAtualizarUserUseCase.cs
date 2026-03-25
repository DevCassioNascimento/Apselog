using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;

namespace Apselog.Application.UseCases.Interfaces;

public interface IAtualizarUserUseCase
{
    Task<UserResponse> ExecutarAsync(AtualizarUserRequest request);
}
