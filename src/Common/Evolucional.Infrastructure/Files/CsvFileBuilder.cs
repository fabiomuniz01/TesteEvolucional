using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Evolucional.Application.Common.Interfaces;
using Evolucional.Application.Dto;
using Evolucional.Infrastructure.Files.Maps;
using CsvHelper;

namespace Evolucional.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildDistrictsFile(IEnumerable<AlunoDto> alunos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Configuration.RegisterClassMap<AlunoMap>();
                csvWriter.WriteRecords(alunos);
            }

            return memoryStream.ToArray();
        }
    }
}
