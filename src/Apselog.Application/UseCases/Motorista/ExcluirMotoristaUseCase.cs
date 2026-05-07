using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Motorista;

public class ExcluirMotoristaUseCase : IExcluirMotoristaUseCase
{
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IUserRepository _userRepository;

    public ExcluirMotoristaUseCase(IMotoristaRepository motoristaRepository, IUserRepository userRepository)
    {
        _motoristaRepository = motoristaRepository;
        _userRepository = userRepository;
    }

    public async Task<ExcluirMotoristaResponse> ExecutarAsync(ExcluirMotoristaRequest request)
    {
        var motorista = await _motoristaRepository.GetByIdAsync(request.Id);

        if (motorista is null)
        {
            throw new KeyNotFoundException("Motorista nao encontrado.");
        }

        if (motorista.UsuarioId.HasValue)
        {
            await _userRepository.DeleteAsync(motorista.UsuarioId.Value);
        }

        await _motoristaRepository.DeleteAsync(request.Id);

        return new ExcluirMotoristaResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "Motorista excluido com sucesso."
        };
    }
}
