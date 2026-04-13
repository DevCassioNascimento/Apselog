using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Logradouro)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Numero)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Complemento)
            .HasMaxLength(100);

        builder.Property(x => x.Bairro)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Estado)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Cep)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Referencia)
            .HasMaxLength(200);

        builder.Property(x => x.Latitude)
            .HasPrecision(10, 7);

        builder.Property(x => x.Longitude)
            .HasPrecision(10, 7);
    }
}
