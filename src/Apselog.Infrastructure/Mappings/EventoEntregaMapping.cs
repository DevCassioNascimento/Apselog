using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class EventoEntregaMapping : IEntityTypeConfiguration<EventoEntrega>
{
    public void Configure(EntityTypeBuilder<EventoEntrega> builder)
    {
        builder.ToTable("EventosEntrega");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.EntregaId)
            .IsRequired();

        builder.Property(x => x.TipoEvento)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.UsuarioId);

        builder.Property(x => x.EtapaChecklistEntregaId);

        builder.Property(x => x.DataEvento)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.MetadataJson)
            .HasMaxLength(4000);

        builder.HasOne(x => x.Entrega)
            .WithMany()
            .HasForeignKey(x => x.EntregaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.EtapaChecklistEntrega)
            .WithMany()
            .HasForeignKey(x => x.EtapaChecklistEntregaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
