using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class EtapaChecklistModeloMapping : IEntityTypeConfiguration<EtapaChecklistModelo>
{
    public void Configure(EntityTypeBuilder<EtapaChecklistModelo> builder)
    {
        builder.ToTable("EtapasChecklistModelo");

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

        builder.Property(x => x.Ordem)
            .IsRequired();

        builder.Property(x => x.Obrigatoria)
            .IsRequired();

        builder.Property(x => x.RequerAssinatura)
            .IsRequired();

        builder.Property(x => x.TipoAssinante)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Ativo)
            .IsRequired();
    }
}
