using Apselog.Domain.Enums;

namespace Apselog.Domain.Entities;

public class Entrega
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Codigo { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Observacoes { get; set; }
    public required string ClienteNome { get; set; }
    public required string ClienteTelefone { get; set; }
    public required string DataPedido { get; set; }
    public string? DataPrevista { get; set; }
    public string? PrevisaoChegada { get; set; }
    public string? DataEntrega { get; set; }
    public Guid? EnderecoId { get; set; }
    public Guid? MotoristaId { get; set; }
    public Motorista? Motorista { get; set; }
    public Guid? VeiculoId { get; set; }
    public Veiculo? Veiculo { get; set; }
    public Guid? DestinatarioUsuarioId { get; set; }
    public User? DestinatarioUsuario { get; set; }
    public EntregaStatus Status { get; set; } = EntregaStatus.Aberto;
}
