using Apselog.Application.DTOs.Request.Endereco;
using Apselog.Application.DTOs.Response.Endereco;
using Apselog.Application.UseCases.Interfaces.Endereco;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Endereco;

public class ExcluirEnderecoUseCase : IExcluirEnderecoUseCase
{
    private readonly IEnderecoRepository _enderecoRepository;

    public ExcluirEnderecoUseCase(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public async Task<ExcluirEnderecoResponse> ExecutarAsync(ExcluirEnderecoRequest request)
    {
        var endereco = await _enderecoRepository.GetByIdAsync(request.Id);

        if (endereco is null)
        {
            throw new KeyNotFoundException("Endereco nao encontrado.");
        }

        await _enderecoRepository.DeleteAsync(request.Id);

        return new ExcluirEnderecoResponse
        {
            Id = request.Id,
            Sucesso = true,
            Mensagem = "Endereco excluido com sucesso."
        };
    }
}
