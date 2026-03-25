using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

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

        builder.Property(x => x.Cargo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Instituicao)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Role)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();
    }
}
