using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases;

public class CriarUserUseCase : ICriarUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CriarUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponse> ExecutarAsync(CriarUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            throw new ArgumentException("O nome do usuário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("O e-mail do usuário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.Senha))
        {
            throw new ArgumentException("A senha do usuário é obrigatória.");
        }

        var usuarioExistente = await _userRepository.GetByEmailAsync(request.Email);

        if (usuarioExistente is not null)
        {
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");
        }

        var user = new User
        {
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = _passwordHasher.HashPassword(request.Senha),
            Cargo = request.Cargo,
            Instituicao = request.Instituicao,
            Role = request.Role,
            Status = request.Status
        };

        await _userRepository.AddAsync(user);

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
