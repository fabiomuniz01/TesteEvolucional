using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Interfaces;
using Evolucional.Application.Common.Security;
using Evolucional.Application.Dto;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Evolucional.Application.Alunos.Queries
{
    public class ExportAlunosQuery : IRequest<ExportDto>
    {
    }

    public class ExportDistrictsQueryHandler : IRequestHandler<ExportAlunosQuery, ExportDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportDistrictsQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportDto> Handle(ExportAlunosQuery request, CancellationToken cancellationToken)
        {
            var result = new ExportDto();

            var records = await _context.Alunos
                //.ThenInclude(c => c.Nome)
                .ProjectToType<AlunoDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            result.Content = _fileBuilder.BuildDistrictsFile(records);
            result.ContentType = "text/csv";
            result.FileName = "Alunos.csv";

            return await Task.FromResult(result);
        }
    }
}
