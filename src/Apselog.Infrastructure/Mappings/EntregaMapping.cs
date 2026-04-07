using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class EntregaMapping : IEntityTypeConfiguration<Entrega>
{
    public void Configure(EntityTypeBuilder<Entrega> builder)
    {
        builder.ToTable("Entregas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Descricao)
            .HasMaxLength(500);

        builder.Property(x => x.Observacoes)
            .HasMaxLength(1000);

        builder.Property(x => x.ClienteNome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.ClienteTelefone)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.DataPedido)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.DataPrevista)
            .HasMaxLength(50);

        builder.Property(x => x.PrevisaoChegada)
            .HasMaxLength(50);

        builder.Property(x => x.DataEntrega)
            .HasMaxLength(50);

        builder.Property(x => x.EnderecoId);

        builder.Property(x => x.MotoristaId);

        builder.Property(x => x.VeiculoId);

        builder.Property(x => x.DestinatarioUsuarioId);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(x => x.Motorista)
            .WithMany(x => x.Entregas)
            .HasForeignKey(x => x.MotoristaId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Veiculo)
            .WithMany()
            .HasForeignKey(x => x.VeiculoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.DestinatarioUsuario)
            .WithMany()
            .HasForeignKey(x => x.DestinatarioUsuarioId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
