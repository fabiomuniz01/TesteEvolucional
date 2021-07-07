using System.Collections.Generic;
using Evolucional.Domain.Entities;
using Mapster;

namespace Evolucional.Application.Dto
{
    public class DisciplinaDto : IRegister
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Disciplina, DisciplinaDto>();
        }
    }
}
