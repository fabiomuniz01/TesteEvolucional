using Evolucional.Domain.Common;

namespace Evolucional.Domain.Entities
{
    public class Nota : AuditableEntity
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public Aluno Aluno { get; set; }

    }
}
