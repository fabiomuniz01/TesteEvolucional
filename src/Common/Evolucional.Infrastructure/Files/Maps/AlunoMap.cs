using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Evolucional.Application.Dto;

namespace Evolucional.Infrastructure.Files.Maps
{
    public sealed class AlunoMap : ClassMap<AlunoDto>
    {
        public AlunoMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
        }
    }
}
