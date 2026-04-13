using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Motorista;

public class CriarMotoristaUseCase : ICriarMotoristaUseCase
{
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CriarMotoristaUseCase(IMotoristaRepository motoristaRepository, IPasswordHasher passwordHasher)
    {
        _motoristaRepository = motoristaRepository;
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

        var motorista = new Domain.Entities.Motorista
        {
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = _passwordHasher.HashPassword(request.Senha),
            Status = request.Status
        };

        await _motoristaRepository.AddAsync(motorista);

        return new CriarMotoristaResponse
        {
            Id = motorista.Id,
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
}
