using System.Security.Cryptography;
using System.Text;
using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases;

public class CriarUserUseCase : ICriarUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CriarUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
            SenhaHash = GerarHashSenha(request.Senha),
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

    private static string GerarHashSenha(string senha)
    {
        var senhaBytes = Encoding.UTF8.GetBytes(senha);
        var hashBytes = SHA256.HashData(senhaBytes);

        return Convert.ToBase64String(hashBytes);
    }
}
