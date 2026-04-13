using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class EtapaChecklistEntregaMapping : IEntityTypeConfiguration<EtapaChecklistEntrega>
{
    public void Configure(EntityTypeBuilder<EtapaChecklistEntrega> builder)
    {
        builder.ToTable("EtapasChecklistEntrega");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.EntregaId)
            .IsRequired();

        builder.Property(x => x.EtapaChecklistModeloId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Concluida)
            .IsRequired();

        builder.Property(x => x.ConcluidaEm)
            .HasMaxLength(50);

        builder.Property(x => x.ConcluidaPorUsuarioId);

        builder.Property(x => x.AssinaturaId);

        builder.Property(x => x.Observacoes)
            .HasMaxLength(1000);

        builder.Property(x => x.Ordem)
            .IsRequired();

        builder.HasOne(x => x.Entrega)
            .WithMany()
            .HasForeignKey(x => x.EntregaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EtapaChecklistModelo)
            .WithMany()
            .HasForeignKey(x => x.EtapaChecklistModeloId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ConcluidaPorUsuario)
            .WithMany()
            .HasForeignKey(x => x.ConcluidaPorUsuarioId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Assinatura)
            .WithMany()
            .HasForeignKey(x => x.AssinaturaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
