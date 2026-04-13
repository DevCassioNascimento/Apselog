using Apselog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apselog.Infrastructure.Mappings;

public class NotificacaoMapping : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.ToTable("Notificacoes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.UsuarioId)
            .IsRequired();

        builder.Property(x => x.EntregaId);

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Mensagem)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Canal)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.LidaEm)
            .HasMaxLength(50);

        builder.Property(x => x.EnviadaEm)
            .HasMaxLength(50);

        builder.Property(x => x.PayloadJson)
            .HasMaxLength(4000);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Entrega)
            .WithMany()
            .HasForeignKey(x => x.EntregaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
