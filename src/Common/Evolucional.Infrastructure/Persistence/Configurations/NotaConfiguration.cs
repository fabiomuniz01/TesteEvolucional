using Evolucional.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evolucional.Infrastructure.Persistence.Configurations
{
    public class NotaConfiguration : IEntityTypeConfiguration<Nota>
    {
        public void Configure(EntityTypeBuilder<Nota> builder)
        {
            builder.Property(t => t.Valor)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(t => t.DisciplinaId)
                .IsRequired();

            builder.Property(t => t.AlunoId)
                .IsRequired();
        }
    }
}
