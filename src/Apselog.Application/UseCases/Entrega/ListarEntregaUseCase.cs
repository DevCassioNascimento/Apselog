using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response.Entrega;
using Apselog.Application.UseCases.Interfaces.Entrega;
using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Entrega;

public class ListarEntregaUseCase : IListarEntregaUseCase
{
    private readonly IEntregaRepository _entregaRepository;

    public ListarEntregaUseCase(IEntregaRepository entregaRepository)
    {
        _entregaRepository = entregaRepository;
    }

    public async Task<IEnumerable<ListarEntregaResponse>> ExecutarAsync(ListarEntregaRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.Entrega> query = await _entregaRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(entrega => entrega.Id == request.Id.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Codigo))
        {
            query = query.Where(entrega =>
                entrega.Codigo.Contains(request.Codigo, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.ClienteNome))
        {
            query = query.Where(entrega =>
                entrega.ClienteNome.Contains(request.ClienteNome, StringComparison.OrdinalIgnoreCase));
        }

        if (request.MotoristaId.HasValue)
        {
            query = query.Where(entrega => entrega.MotoristaId == request.MotoristaId.Value);
        }

        if (request.VeiculoId.HasValue)
        {
            query = query.Where(entrega => entrega.VeiculoId == request.VeiculoId.Value);
        }

        if (request.DestinatarioUsuarioId.HasValue)
        {
            query = query.Where(entrega => entrega.DestinatarioUsuarioId == request.DestinatarioUsuarioId.Value);
        }

        if (request.EnderecoId.HasValue)
        {
            query = query.Where(entrega => entrega.EnderecoId == request.EnderecoId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(entrega => entrega.Status == request.Status.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(entrega => new ListarEntregaResponse
        {
            Id = entrega.Id,
            Codigo = entrega.Codigo,
            Descricao = entrega.Descricao,
            Observacoes = entrega.Observacoes,
            ClienteNome = entrega.ClienteNome,
            ClienteTelefone = entrega.ClienteTelefone,
            DataPedido = entrega.DataPedido,
            EnderecoId = entrega.EnderecoId,
            Endereco = entrega.Endereco is null
                ? null
                : new EnderecoEntregaResponse
                {
                    Id = entrega.Endereco.Id,
                    Logradouro = entrega.Endereco.Logradouro,
                    Numero = entrega.Endereco.Numero,
                    Complemento = entrega.Endereco.Complemento,
                    Bairro = entrega.Endereco.Bairro,
                    Cidade = entrega.Endereco.Cidade,
                    Estado = entrega.Endereco.Estado,
                    Cep = entrega.Endereco.Cep,
                    Referencia = entrega.Endereco.Referencia,
                    Latitude = entrega.Endereco.Latitude,
                    Longitude = entrega.Endereco.Longitude
                },
            MotoristaId = entrega.MotoristaId,
            VeiculoId = entrega.VeiculoId,
            DestinatarioUsuarioId = entrega.DestinatarioUsuarioId,
            Status = entrega.Status
        });
    }

    private static IEnumerable<Domain.Entities.Entrega> AplicarOrdenacao(
        IEnumerable<Domain.Entities.Entrega> entregas,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? entregas.OrderBy(entrega => entrega.Codigo)
                : entregas.OrderByDescending(entrega => entrega.Codigo);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? entregas.OrderBy(entrega => entrega.Id) : entregas.OrderByDescending(entrega => entrega.Id),
            "codigo" => ascendente ? entregas.OrderBy(entrega => entrega.Codigo) : entregas.OrderByDescending(entrega => entrega.Codigo),
            "clientenome" => ascendente ? entregas.OrderBy(entrega => entrega.ClienteNome) : entregas.OrderByDescending(entrega => entrega.ClienteNome),
            "clientetelefone" => ascendente ? entregas.OrderBy(entrega => entrega.ClienteTelefone) : entregas.OrderByDescending(entrega => entrega.ClienteTelefone),
            "datapedido" => ascendente ? entregas.OrderBy(entrega => entrega.DataPedido) : entregas.OrderByDescending(entrega => entrega.DataPedido),
            "motoristaid" => ascendente ? entregas.OrderBy(entrega => entrega.MotoristaId) : entregas.OrderByDescending(entrega => entrega.MotoristaId),
            "veiculoid" => ascendente ? entregas.OrderBy(entrega => entrega.VeiculoId) : entregas.OrderByDescending(entrega => entrega.VeiculoId),
            "status" => ascendente ? entregas.OrderBy(entrega => entrega.Status) : entregas.OrderByDescending(entrega => entrega.Status),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
