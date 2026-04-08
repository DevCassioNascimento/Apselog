namespace Apselog.Application.DTOs.Response.Endereco;

public class ExcluirEnderecoResponse
{
    public Guid Id { get; set; }
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
