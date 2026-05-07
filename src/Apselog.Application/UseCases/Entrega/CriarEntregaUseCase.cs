using Apselog.Application.DTOs.Request;
using Apselog.Application.DTOs.Response.Entrega;
using Apselog.Domain.Interfaces.Repositories;
using Apselog.Application.UseCases.Interfaces.Entrega;

namespace Apselog.Application.UseCases.Entrega;

public class CriarEntregaUseCase : ICriarEntregaUseCase
{
    private readonly IEntregaRepository _entregaRepository;
    private readonly IUserRepository _userRepository;

    public CriarEntregaUseCase(IEntregaRepository entregaRepository, IUserRepository userRepository)
    {
        _entregaRepository = entregaRepository;
        _userRepository = userRepository;
    }

    public async Task<CriarEntregaResponse> ExecutarAsync(CriarEntregaRequest request)
    {
        await ValidarRequestAsync(request);

        var entrega = new Domain.Entities.Entrega
        {
            Codigo = request.Codigo,
            Descricao = request.Descricao,
            Observacoes = request.Observacoes,
            ClienteNome = request.ClienteNome,
            ClienteTelefone = request.ClienteTelefone,
            DataPedido = request.DataPedido,
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

    private async Task ValidarRequestAsync(CriarEntregaRequest request)
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
