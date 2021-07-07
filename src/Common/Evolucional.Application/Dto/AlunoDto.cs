using System.Collections.Generic;
using Evolucional.Domain.Entities;
using Mapster;

namespace Evolucional.Application.Dto
{
    public class AlunoDto : IRegister 
    {
        public AlunoDto()
        {
            Disciplinas = new List<DisciplinaDto>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public IList<DisciplinaDto> Disciplinas { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Aluno, AlunoDto>();
        }
    }
}
