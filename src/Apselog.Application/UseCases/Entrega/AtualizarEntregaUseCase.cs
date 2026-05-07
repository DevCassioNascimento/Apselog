using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response.Entrega;
using Apselog.Application.UseCases.Interfaces.Entrega;
using Apselog.Domain.Interfaces.Repositories;

namespace Apselog.Application.UseCases.Entrega;

public class AtualizarEntregaUseCase : IAtualizarEntregaUseCase
{
    private readonly IEntregaRepository _entregaRepository;
    private readonly IUserRepository _userRepository;

    public AtualizarEntregaUseCase(IEntregaRepository entregaRepository, IUserRepository userRepository)
    {
        _entregaRepository = entregaRepository;
        _userRepository = userRepository;
    }

    public async Task<AtualizarEntregaResponse> ExecutarAsync(AtualizarEntregaRequest request)
    {
        var entrega = await _entregaRepository.GetByIdAsync(request.Id);

        if (entrega is null)
        {
            throw new KeyNotFoundException("Entrega nao encontrada.");
        }

        await ValidarRequestAsync(request);

        entrega.Codigo = request.Codigo;
        entrega.Descricao = request.Descricao;
        entrega.Observacoes = request.Observacoes;
        entrega.ClienteNome = request.ClienteNome;
        entrega.ClienteTelefone = request.ClienteTelefone;
        entrega.DataPedido = request.DataPedido;
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
            Descricao = entrega.Descricao,
            Observacoes = entrega.Observacoes,
            ClienteNome = entrega.ClienteNome,
            ClienteTelefone = entrega.ClienteTelefone,
            DataPedido = entrega.DataPedido,
            EnderecoId = entrega.EnderecoId,
            MotoristaId = entrega.MotoristaId,
            VeiculoId = entrega.VeiculoId,
            DestinatarioUsuarioId = entrega.DestinatarioUsuarioId,
            Status = entrega.Status
        };
    }

    private async Task ValidarRequestAsync(AtualizarEntregaRequest request)
    {
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

        if (!request.DestinatarioUsuarioId.HasValue)
        {
            return;
        }

        var destinatario = await _userRepository.GetByIdAsync(request.DestinatarioUsuarioId.Value);

        if (destinatario is null)
        {
            throw new KeyNotFoundException("Usuario recebedor nao encontrado.");
        }
    }
}
