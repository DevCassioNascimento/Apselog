using System.Security.Cryptography;
using System.Text;
using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Motorista;

public class AtualizarMotoristaUseCase : IAtualizarMotoristaUseCase
{
    private readonly IMotoristaRepository _motoristaRepository;

    public AtualizarMotoristaUseCase(IMotoristaRepository motoristaRepository)
    {
        _motoristaRepository = motoristaRepository;
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

        motorista.Nome = request.Nome;
        motorista.Email = request.Email;
        motorista.Status = request.Status;

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            motorista.SenhaHash = GerarHashSenha(request.Senha);
        }

        await _motoristaRepository.UpdateAsync(motorista);

        return new AtualizarMotoristaResponse
        {
            Id = motorista.Id,
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

    private static string GerarHashSenha(string senha)
    {
        var senhaBytes = Encoding.UTF8.GetBytes(senha);
        var hashBytes = SHA256.HashData(senhaBytes);

        return Convert.ToBase64String(hashBytes);
    }
}
