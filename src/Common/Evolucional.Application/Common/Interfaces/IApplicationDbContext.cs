using System.Threading;
using System.Threading.Tasks;
using Evolucional.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Evolucional.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Aluno> Alunos { get; set; }

        DbSet<Disciplina> Disciplinas { get; set; }

        DbSet<Nota> Notas { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
