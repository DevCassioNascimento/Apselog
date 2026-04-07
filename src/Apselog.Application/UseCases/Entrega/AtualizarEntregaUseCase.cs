using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response.Entrega;
using Apselog.Application.UseCases.Interfaces.Entrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Entrega;

public class AtualizarEntregaUseCase : IAtualizarEntregaUseCase
{
    private readonly IEntregaRepository _entregaRepository;

    public AtualizarEntregaUseCase(IEntregaRepository entregaRepository)
    {
        _entregaRepository = entregaRepository;
    }

    public async Task<AtualizarEntregaResponse> ExecutarAsync(AtualizarEntregaRequest request)
    {
        var entrega = await _entregaRepository.GetByIdAsync(request.Id);

        if (entrega is null)
        {
            throw new KeyNotFoundException("Entrega nao encontrada.");
        }

        ValidarRequest(request);

        entrega.Codigo = request.Codigo;
        entrega.Nome = request.Nome;
        entrega.Descricao = request.Descricao;
        entrega.Observacoes = request.Observacoes;
        entrega.ClienteNome = request.ClienteNome;
        entrega.ClienteTelefone = request.ClienteTelefone;
        entrega.DataPedido = request.DataPedido;
        entrega.DataPrevista = request.DataPrevista;
        entrega.PrevisaoChegada = request.PrevisaoChegada;
        entrega.DataEntrega = request.DataEntrega;
        entrega.EnderecoId = request.EnderecoId;
        entrega.MotoristaId = request.MotoristaId;
        entrega.VeiculoId = request.VeiculoId;
        entrega.DestinatarioUsuarioId = request.DestinatarioUsuarioId;
        entrega.Status = request.Status;

        await _entregaRepository.UpdateAsync(entrega);

        return new AtualizarEntregaResponse
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

    private static void ValidarRequest(AtualizarEntregaRequest request)
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
