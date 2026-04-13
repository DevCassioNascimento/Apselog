using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response;
using Apselog.Application.UseCases.Interfaces;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUseCase(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> ExecutarAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("O e-mail e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Senha))
        {
            throw new ArgumentException("A senha e obrigatoria.");
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("E-mail ou senha invalidos.");
        }

        if (!_passwordHasher.VerifyPassword(request.Senha, user.SenhaHash))
        {
            throw new UnauthorizedAccessException("E-mail ou senha invalidos.");
        }

        var token = _jwtProvider.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            Cargo = user.Cargo,
            Instituicao = user.Instituicao,
            Role = user.Role,
            Status = user.Status
        };
    }
}
