using Apselog.Domain.Enums;

namespace Apselog.Application.DTOs.Response.Veiculo;

public class CriarVeiculoResponse
{
    public Guid Id { get; set; }
    public required string Placa { get; set; }
    public required string Modelo { get; set; }
    public required string Tipo { get; set; }
    public VeiculoStatus Status { get; set; }
    public Guid MotoristaId { get; set; }
}
