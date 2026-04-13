using Apselog.Application.DTOs.Request.Endereco;
using Apselog.Application.DTOs.Response.Endereco;
using Apselog.Application.UseCases.Interfaces.Endereco;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Endereco;

public class AtualizarEnderecoUseCase : IAtualizarEnderecoUseCase
{
    private readonly IEnderecoRepository _enderecoRepository;

    public AtualizarEnderecoUseCase(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public async Task<AtualizarEnderecoResponse> ExecutarAsync(AtualizarEnderecoRequest request)
    {
        var endereco = await _enderecoRepository.GetByIdAsync(request.Id);

        if (endereco is null)
        {
            throw new KeyNotFoundException("Endereco nao encontrado.");
        }

        ValidarRequest(request);

        endereco.Logradouro = request.Logradouro;
        endereco.Numero = request.Numero;
        endereco.Complemento = request.Complemento;
        endereco.Bairro = request.Bairro;
        endereco.Cidade = request.Cidade;
        endereco.Estado = request.Estado;
        endereco.Cep = request.Cep;
        endereco.Referencia = request.Referencia;
        endereco.Latitude = request.Latitude;
        endereco.Longitude = request.Longitude;

        await _enderecoRepository.UpdateAsync(endereco);

        return new AtualizarEnderecoResponse
        {
            Id = endereco.Id,
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero,
            Complemento = endereco.Complemento,
            Bairro = endereco.Bairro,
            Cidade = endereco.Cidade,
            Estado = endereco.Estado,
            Cep = endereco.Cep,
            Referencia = endereco.Referencia,
            Latitude = endereco.Latitude,
            Longitude = endereco.Longitude
        };
    }

    private static void ValidarRequest(AtualizarEnderecoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Logradouro))
        {
            throw new ArgumentException("O logradouro e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Numero))
        {
            throw new ArgumentException("O numero e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Bairro))
        {
            throw new ArgumentException("O bairro e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Cidade))
        {
            throw new ArgumentException("A cidade e obrigatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.Estado))
        {
            throw new ArgumentException("O estado e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Cep))
        {
            throw new ArgumentException("O CEP e obrigatorio.");
        }
    }
}
