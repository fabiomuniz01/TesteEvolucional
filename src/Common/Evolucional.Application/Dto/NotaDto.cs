using Evolucional.Domain.Entities;
using Mapster;

namespace Evolucional.Application.Dto
{
    public class NotaDto : IRegister
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DisciplinaDto Disciplina { get; set; }
        public AlunoDto Aluno { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Nota, NotaDto>();
        }
    }
}
