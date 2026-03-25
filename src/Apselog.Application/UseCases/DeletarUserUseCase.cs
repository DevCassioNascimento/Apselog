using Apselog.Application.DTOs.Request;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases;

public class DeletarUserUseCase : IDeletarUserUseCase
{
    private readonly IUserRepository _userRepository;

    public DeletarUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecutarAsync(DeletarUserRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            throw new KeyNotFoundException("Usuario nao encontrado.");
        }

        await _userRepository.DeleteAsync(request.Id);
    }
}
