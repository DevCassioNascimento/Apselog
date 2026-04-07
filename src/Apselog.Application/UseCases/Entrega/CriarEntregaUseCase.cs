using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response.Entrega;
using Apselog.Application.UseCases.Interfaces.Entrega;
using Apselog.Domain.Entities;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Entrega;

public class CriarEntregaUseCase : ICriarEntregaUseCase
{
    private readonly IEntregaRepository _entregaRepository;

    public CriarEntregaUseCase(IEntregaRepository entregaRepository)
    {
        _entregaRepository = entregaRepository;
    }

    public async Task<CriarEntregaResponse> ExecutarAsync(CriarEntregaRequest request)
    {
        ValidarRequest(request);

        var entrega = new Domain.Entities.Entrega
        {
            Codigo = request.Codigo,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Observacoes = request.Observacoes,
            ClienteNome = request.ClienteNome,
            ClienteTelefone = request.ClienteTelefone,
            DataPedido = request.DataPedido,
            DataPrevista = request.DataPrevista,
            PrevisaoChegada = request.PrevisaoChegada,
            DataEntrega = request.DataEntrega,
            EnderecoId = request.EnderecoId,
            MotoristaId = request.MotoristaId,
            VeiculoId = request.VeiculoId,
            DestinatarioUsuarioId = request.DestinatarioUsuarioId,
            Status = request.Status
        };

        await _entregaRepository.AddAsync(entrega);

        return new CriarEntregaResponse
        {
            Id = entrega.Id,
            Codigo = entrega.Codigo,
            Nome = entrega.Nome,
            Descricao = entrega.Descricao,
            Observacoes = entrega.Observacoes,
            ClienteNome = entrega.ClienteNome,
            ClienteTelefone = entrega.ClienteTelefone,
            DataPedido = entrega.DataPedido,
            DataPrevista = entrega.DataPrevista,
            PrevisaoChegada = entrega.PrevisaoChegada,
            DataEntrega = entrega.DataEntrega,
            EnderecoId = entrega.EnderecoId,
            MotoristaId = entrega.MotoristaId,
            VeiculoId = entrega.VeiculoId,
            DestinatarioUsuarioId = entrega.DestinatarioUsuarioId,
            Status = entrega.Status
        };
    }

    private static void ValidarRequest(CriarEntregaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            throw new ArgumentException("O nome da entrega e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Codigo))
        {
            throw new ArgumentException("O codigo da entrega e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.ClienteNome))
        {
            throw new ArgumentException("O nome do cliente e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.ClienteTelefone))
        {
            throw new ArgumentException("O telefone do cliente e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.DataPedido))
        {
            throw new ArgumentException("A data do pedido e obrigatoria.");
        }
    }
}
