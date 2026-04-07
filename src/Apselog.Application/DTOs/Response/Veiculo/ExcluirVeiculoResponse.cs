namespace Apselog.Application.DTOs.Response.Veiculo;

public class ExcluirVeiculoResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
