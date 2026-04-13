using Apselog.Application.DTOs.Request.Endereco;
using Apselog.Application.DTOs.Response.Endereco;
using Apselog.Application.UseCases.Interfaces.Endereco;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Endereco;

public class CriarEnderecoUseCase : ICriarEnderecoUseCase
{
    private readonly IEnderecoRepository _enderecoRepository;

    public CriarEnderecoUseCase(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public async Task<CriarEnderecoResponse> ExecutarAsync(CriarEnderecoRequest request)
    {
        ValidarRequest(request);

        var endereco = new Domain.Entities.Endereco
        {
            Logradouro = request.Logradouro,
            Numero = request.Numero,
            Complemento = request.Complemento,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Estado = request.Estado,
            Cep = request.Cep,
            Referencia = request.Referencia,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        await _enderecoRepository.AddAsync(endereco);

        return new CriarEnderecoResponse
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

    private static void ValidarRequest(CriarEnderecoRequest request)
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
