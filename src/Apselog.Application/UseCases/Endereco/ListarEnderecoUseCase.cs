using Apselog.Application.DTOs.Request.Endereco;
using Apselog.Application.DTOs.Response.Endereco;
using Apselog.Application.UseCases.Interfaces.Endereco;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Endereco;

public class ListarEnderecoUseCase : IListarEnderecoUseCase
{
    private readonly IEnderecoRepository _enderecoRepository;

    public ListarEnderecoUseCase(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public async Task<IEnumerable<ListarEnderecoResponse>> ExecutarAsync(ListarEnderecoRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.Endereco> query = await _enderecoRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(endereco => endereco.Id == request.Id.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Logradouro))
        {
            query = query.Where(endereco =>
                endereco.Logradouro.Contains(request.Logradouro, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Numero))
        {
            query = query.Where(endereco =>
                endereco.Numero.Contains(request.Numero, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Bairro))
        {
            query = query.Where(endereco =>
                endereco.Bairro.Contains(request.Bairro, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Cidade))
        {
            query = query.Where(endereco =>
                endereco.Cidade.Contains(request.Cidade, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Estado))
        {
            query = query.Where(endereco =>
                endereco.Estado.Contains(request.Estado, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Cep))
        {
            query = query.Where(endereco =>
                endereco.Cep.Contains(request.Cep, StringComparison.OrdinalIgnoreCase));
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(endereco => new ListarEnderecoResponse
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
        });
    }

    private static IEnumerable<Domain.Entities.Endereco> AplicarOrdenacao(
        IEnumerable<Domain.Entities.Endereco> enderecos,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? enderecos.OrderBy(endereco => endereco.Logradouro)
                : enderecos.OrderByDescending(endereco => endereco.Logradouro);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? enderecos.OrderBy(endereco => endereco.Id) : enderecos.OrderByDescending(endereco => endereco.Id),
            "logradouro" => ascendente ? enderecos.OrderBy(endereco => endereco.Logradouro) : enderecos.OrderByDescending(endereco => endereco.Logradouro),
            "numero" => ascendente ? enderecos.OrderBy(endereco => endereco.Numero) : enderecos.OrderByDescending(endereco => endereco.Numero),
            "bairro" => ascendente ? enderecos.OrderBy(endereco => endereco.Bairro) : enderecos.OrderByDescending(endereco => endereco.Bairro),
            "cidade" => ascendente ? enderecos.OrderBy(endereco => endereco.Cidade) : enderecos.OrderByDescending(endereco => endereco.Cidade),
            "estado" => ascendente ? enderecos.OrderBy(endereco => endereco.Estado) : enderecos.OrderByDescending(endereco => endereco.Estado),
            "cep" => ascendente ? enderecos.OrderBy(endereco => endereco.Cep) : enderecos.OrderByDescending(endereco => endereco.Cep),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
