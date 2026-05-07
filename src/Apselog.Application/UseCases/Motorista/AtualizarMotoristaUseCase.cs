using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Domain.Entities;
using Apselog.Domain.Enums;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Motorista;

public class AtualizarMotoristaUseCase : IAtualizarMotoristaUseCase
{
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AtualizarMotoristaUseCase(
        IMotoristaRepository motoristaRepository,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _motoristaRepository = motoristaRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AtualizarMotoristaResponse> ExecutarAsync(AtualizarMotoristaRequest request)
    {
        var motorista = await _motoristaRepository.GetByIdAsync(request.Id);

        if (motorista is null)
        {
            throw new KeyNotFoundException("Motorista nao encontrado.");
        }

        ValidarRequest(request);

        var motoristaComMesmoEmail = await _motoristaRepository.GetByEmailAsync(request.Email);

        if (motoristaComMesmoEmail is not null && motoristaComMesmoEmail.Id != request.Id)
        {
            throw new InvalidOperationException("Ja existe um motorista cadastrado com este e-mail.");
        }

        var linkedUser = await ResolveOrCreateLinkedUserAsync(motorista, request);

        motorista.Nome = request.Nome;
        motorista.Email = request.Email;
        motorista.UsuarioId = linkedUser.Id;
        motorista.Status = request.Status;

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            motorista.SenhaHash = _passwordHasher.HashPassword(request.Senha);
        }

        await _motoristaRepository.UpdateAsync(motorista);

        return new AtualizarMotoristaResponse
        {
            Id = motorista.Id,
            UsuarioId = motorista.UsuarioId,
            Nome = motorista.Nome,
            Email = motorista.Email,
            Status = motorista.Status
        };
    }

    private static void ValidarRequest(AtualizarMotoristaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            throw new ArgumentException("O nome do motorista e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("O e-mail do motorista e obrigatorio.");
        }
    }

    private async Task<User> ResolveOrCreateLinkedUserAsync(
        Domain.Entities.Motorista motorista,
        AtualizarMotoristaRequest request)
    {
        var linkedUser = motorista.UsuarioId.HasValue
            ? await _userRepository.GetByIdAsync(motorista.UsuarioId.Value)
            : null;

        if (linkedUser is null && request.UsuarioId.HasValue)
        {
            linkedUser = await _userRepository.GetByIdAsync(request.UsuarioId.Value);

            if (linkedUser is null)
            {
                throw new KeyNotFoundException("Usuario vinculado ao motorista nao foi encontrado.");
            }
        }

        var userByEmail = await _userRepository.GetByEmailAsync(request.Email);

        if (userByEmail is not null && userByEmail.Id != linkedUser?.Id)
        {
            throw new InvalidOperationException("Ja existe um usuario cadastrado com este e-mail.");
        }

        if (linkedUser is null)
        {
            linkedUser = new User
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = !string.IsNullOrWhiteSpace(request.Senha)
                    ? _passwordHasher.HashPassword(request.Senha)
                    : motorista.SenhaHash,
                Cargo = "Motorista",
                Instituicao = "APSeLog",
                Role = UserRole.Usuario,
                Status = MapMotoristaStatus(request.Status)
            };

            await _userRepository.AddAsync(linkedUser);
            return linkedUser;
        }

        linkedUser.Nome = request.Nome;
        linkedUser.Email = request.Email;
        linkedUser.Cargo = "Motorista";
        linkedUser.Role = UserRole.Usuario;
        linkedUser.Status = MapMotoristaStatus(request.Status);

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            linkedUser.SenhaHash = _passwordHasher.HashPassword(request.Senha);
        }

        if (string.IsNullOrWhiteSpace(linkedUser.Instituicao))
        {
            linkedUser.Instituicao = "APSeLog";
        }

        await _userRepository.UpdateAsync(linkedUser);
        return linkedUser;
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
