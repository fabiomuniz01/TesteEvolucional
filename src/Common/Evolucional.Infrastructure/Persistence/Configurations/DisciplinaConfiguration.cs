using Evolucional.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evolucional.Infrastructure.Persistence.Configurations
{
    public class DisciplinaConfiguration : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.Property(t => t.Nome)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
