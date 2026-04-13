using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class ItemEntregaMapping : IEntityTypeConfiguration<ItemEntrega>
{
    public void Configure(EntityTypeBuilder<ItemEntrega> builder)
    {
        builder.ToTable("ItensEntrega");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.EntregaId)
            .IsRequired();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Descricao)
            .HasMaxLength(500);

        builder.Property(x => x.Sku)
            .HasMaxLength(100);

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.Unidade)
            .HasMaxLength(30);

        builder.Property(x => x.ValorDeclarado)
            .HasPrecision(18, 2);

        builder.Property(x => x.Ordem)
            .IsRequired();

        builder.HasOne(x => x.Entrega)
            .WithMany()
            .HasForeignKey(x => x.EntregaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
