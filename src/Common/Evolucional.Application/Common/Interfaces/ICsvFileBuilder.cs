using System.Collections.Generic;
using Evolucional.Application.Dto;

namespace Evolucional.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildDistrictsFile(IEnumerable<AlunoDto> alunos);
    }
}
