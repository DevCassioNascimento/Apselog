using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases;

public class AtualizarUserUseCase : IAtualizarUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AtualizarUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponse> ExecutarAsync(AtualizarUserRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            throw new KeyNotFoundException("Usuario nao encontrado.");
        }

        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            throw new ArgumentException("O nome do usuario e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("O e-mail do usuario e obrigatorio.");
        }

        var usuarioComMesmoEmail = await _userRepository.GetByEmailAsync(request.Email);

        if (usuarioComMesmoEmail is not null && usuarioComMesmoEmail.Id != request.Id)
        {
            throw new InvalidOperationException("Ja existe um usuario cadastrado com este e-mail.");
        }

        user.Nome = request.Nome;
        user.Email = request.Email;
        user.Cargo = request.Cargo;
        user.Instituicao = request.Instituicao;
        user.Role = request.Role;
        user.Status = request.Status;

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            user.SenhaHash = _passwordHasher.HashPassword(request.Senha);
        }

        await _userRepository.UpdateAsync(user);

        return new UserResponse
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            Cargo = user.Cargo,
            Instituicao = user.Instituicao,
            Role = user.Role,
            Status = user.Status
        };
    }
}
