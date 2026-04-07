using Apselog.Application.DTOs.Request.Motorista;
using Apselog.Application.DTOs.Response.Motorista;
using Apselog.Application.UseCases.Interfaces.Motorista;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Motorista;

public class ListarMotoristaUseCase : IListarMotoristaUseCase
{
    private readonly IMotoristaRepository _motoristaRepository;

    public ListarMotoristaUseCase(IMotoristaRepository motoristaRepository)
    {
        _motoristaRepository = motoristaRepository;
    }

    public async Task<IEnumerable<ListarMotoristaResponse>> ExecutarAsync(ListarMotoristaRequest request)
    {
        if (request.Page.HasValue && request.Page <= 0)
        {
            throw new ArgumentException("Page deve ser maior que zero.");
        }

        if (request.PageSize.HasValue && request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize deve ser maior que zero.");
        }

        IEnumerable<Domain.Entities.Motorista> query = await _motoristaRepository.GetAllAsync();

        if (request.Id.HasValue)
        {
            query = query.Where(motorista => motorista.Id == request.Id.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Nome))
        {
            query = query.Where(motorista =>
                motorista.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            query = query.Where(motorista =>
                motorista.Email.Contains(request.Email, StringComparison.OrdinalIgnoreCase));
        }

        if (request.Status.HasValue)
        {
            query = query.Where(motorista => motorista.Status == request.Status.Value);
        }

        query = AplicarOrdenacao(query, request.OrdenarPor, request.Ascendente);

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        return query.Select(motorista => new ListarMotoristaResponse
        {
            Id = motorista.Id,
            Nome = motorista.Nome,
            Email = motorista.Email,
            Status = motorista.Status
        });
    }

    private static IEnumerable<Domain.Entities.Motorista> AplicarOrdenacao(
        IEnumerable<Domain.Entities.Motorista> motoristas,
        string? ordenarPor,
        bool ascendente)
    {
        if (string.IsNullOrWhiteSpace(ordenarPor))
        {
            return ascendente
                ? motoristas.OrderBy(motorista => motorista.Nome)
                : motoristas.OrderByDescending(motorista => motorista.Nome);
        }

        return ordenarPor.Trim().ToLowerInvariant() switch
        {
            "id" => ascendente ? motoristas.OrderBy(motorista => motorista.Id) : motoristas.OrderByDescending(motorista => motorista.Id),
            "nome" => ascendente ? motoristas.OrderBy(motorista => motorista.Nome) : motoristas.OrderByDescending(motorista => motorista.Nome),
            "email" => ascendente ? motoristas.OrderBy(motorista => motorista.Email) : motoristas.OrderByDescending(motorista => motorista.Email),
            "status" => ascendente ? motoristas.OrderBy(motorista => motorista.Status) : motoristas.OrderByDescending(motorista => motorista.Status),
            _ => throw new ArgumentException("Campo de ordenacao invalido.")
        };
    }
}
