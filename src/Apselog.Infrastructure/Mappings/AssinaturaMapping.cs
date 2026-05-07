using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class AssinaturaMapping : IEntityTypeConfiguration<Assinatura>
{
    public void Configure(EntityTypeBuilder<Assinatura> builder)
    {
        builder.ToTable("Assinatura");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.EntregaId)
            .IsRequired();

        builder.Property(x => x.EtapaChecklistEntregaId);

        builder.Property(x => x.AssinadoPorNome)
            .IsRequired();

        builder.Property(x => x.AssinadoPorDocumento);

        builder.Property(x => x.AssinadoPorTipo)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.ImagemBase64);

        builder.Property(x => x.FotoEntregaBase64);

        builder.Property(x => x.ArquivoUrl);

        builder.Property(x => x.IpOrigem);

        builder.Property(x => x.DeviceInfo);

        builder.Property(x => x.AssinadoEm)
            .IsRequired();

        builder.HasOne(x => x.Entrega)
            .WithMany()
            .HasForeignKey(x => x.EntregaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EtapaChecklistEntrega)
            .WithMany()
            .HasForeignKey(x => x.EtapaChecklistEntregaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
