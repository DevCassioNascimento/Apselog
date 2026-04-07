namespace Apselog.Domain.Entities;

public class EtapaChecklistModelo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Codigo { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public int Ordem { get; set; }
    public bool Obrigatoria { get; set; }
    public bool RequerAssinatura { get; set; }
    public string TipoAssinante { get; set; } = "NENHUM";
    public bool Ativo { get; set; } = true;
}
