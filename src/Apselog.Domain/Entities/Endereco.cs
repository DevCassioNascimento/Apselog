namespace Apselog.Domain.Entities;

public class Endereco
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Logradouro { get; set; }
    public required string Numero { get; set; }
    public string? Complemento { get; set; }
    public required string Bairro { get; set; }
    public required string Cidade { get; set; }
    public required string Estado { get; set; }
    public required string Cep { get; set; }
    public string? Referencia { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
