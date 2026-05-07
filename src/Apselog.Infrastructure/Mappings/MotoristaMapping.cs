using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class MotoristaMapping : IEntityTypeConfiguration<Motorista>
{
    public void Configure(EntityTypeBuilder<Motorista> builder)
    {
        builder.ToTable("Motoristas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.SenhaHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.UsuarioId);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Veiculos)
            .WithOne(x => x.Motorista)
            .HasForeignKey(x => x.MotoristaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
