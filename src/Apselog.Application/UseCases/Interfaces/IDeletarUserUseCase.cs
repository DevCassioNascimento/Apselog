using Apselog.Application.DTOs.Request;

namespace Apselog.Application.UseCases.Interfaces;

public interface IDeletarUserUseCase
{
    Task ExecutarAsync(DeletarUserRequest request);
}
