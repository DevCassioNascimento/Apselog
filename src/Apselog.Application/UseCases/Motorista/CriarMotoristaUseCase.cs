using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Domain.Entities;
using Apselog.Domain.Enums;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Motorista;

public class CriarMotoristaUseCase : ICriarMotoristaUseCase
{
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CriarMotoristaUseCase(
        IMotoristaRepository motoristaRepository,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _motoristaRepository = motoristaRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<CriarMotoristaResponse> ExecutarAsync(CriarMotoristaRequest request)
    {
        ValidarRequest(request);

        var motoristaExistente = await _motoristaRepository.GetByEmailAsync(request.Email);

        if (motoristaExistente is not null)
        {
            throw new InvalidOperationException("Ja existe um motorista cadastrado com este e-mail.");
        }

        var linkedUser = await ResolveOrCreateLinkedUserAsync(request);

        var motorista = new Domain.Entities.Motorista
        {
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = _passwordHasher.HashPassword(request.Senha),
            UsuarioId = linkedUser.Id,
            Status = request.Status
        };

        await _motoristaRepository.AddAsync(motorista);

        return new CriarMotoristaResponse
        {
            Id = motorista.Id,
            UsuarioId = motorista.UsuarioId,
            Nome = motorista.Nome,
            Email = motorista.Email,
            Status = motorista.Status
        };
    }

    private static void ValidarRequest(CriarMotoristaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            throw new ArgumentException("O nome do motorista e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("O e-mail do motorista e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Senha))
        {
            throw new ArgumentException("A senha do motorista e obrigatoria.");
        }
    }

    private async Task<User> ResolveOrCreateLinkedUserAsync(CriarMotoristaRequest request)
    {
        var userByEmail = await _userRepository.GetByEmailAsync(request.Email);
        User? user = null;

        if (request.UsuarioId.HasValue)
        {
            user = await _userRepository.GetByIdAsync(request.UsuarioId.Value);

            if (user is null)
            {
                throw new KeyNotFoundException("Usuario vinculado ao motorista nao foi encontrado.");
            }

            if (userByEmail is not null && userByEmail.Id != user.Id)
            {
                throw new InvalidOperationException("Ja existe um usuario cadastrado com este e-mail.");
            }
        }
        else if (userByEmail is not null)
        {
            throw new InvalidOperationException("Ja existe um usuario cadastrado com este e-mail.");
        }

        if (user is null)
        {
            user = new User
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = _passwordHasher.HashPassword(request.Senha),
                Cargo = "Motorista",
                Instituicao = "APSeLog",
                Role = UserRole.Usuario,
                Status = MapMotoristaStatus(request.Status)
            };

            await _userRepository.AddAsync(user);
            return user;
        }

        user.Nome = request.Nome;
        user.Email = request.Email;
        user.SenhaHash = _passwordHasher.HashPassword(request.Senha);
        user.Cargo = "Motorista";
        user.Role = UserRole.Usuario;
        user.Status = MapMotoristaStatus(request.Status);

        if (string.IsNullOrWhiteSpace(user.Instituicao))
        {
            user.Instituicao = "APSeLog";
        }

        await _userRepository.UpdateAsync(user);
        return user;
    }

    private static UserStatus MapMotoristaStatus(MotoristaStatus status)
    {
        return status switch
        {
            MotoristaStatus.Inativo => UserStatus.Inativo,
            MotoristaStatus.Bloqueado => UserStatus.Bloqueado,
            _ => UserStatus.Ativo,
        };
    }
}
