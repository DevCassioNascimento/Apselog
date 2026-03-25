using System.Security.Cryptography;
using System.Text;
using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases;

public class AtualizarUserUseCase : IAtualizarUserUseCase
{
    private readonly IUserRepository _userRepository;

    public AtualizarUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        user.Status = request.Status;

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            user.SenhaHash = GerarHashSenha(request.Senha);
        }

        await _userRepository.UpdateAsync(user);

        return new UserResponse
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            Cargo = user.Cargo,
            Instituicao = user.Instituicao,
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
