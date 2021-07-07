using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Interfaces;
using Evolucional.Application.Common.Mapping;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Evolucional.Application.Disciplinas.Queries.GetDisciplinasComPaginacao
{
    public class GetAllDisciplinasComPaginacaoQuery : IRequestWrapper<PaginatedList<DisciplinaDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

    public class GetCitiesQueryHandler : IRequestHandlerWrapper<GetAllDisciplinasComPaginacaoQuery, PaginatedList<DisciplinaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<DisciplinaDto>>> Handle(GetAllDisciplinasComPaginacaoQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<DisciplinaDto> list = await _context.Disciplinas
                .ProjectToType<DisciplinaDto>(_mapper.Config)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<DisciplinaDto>>(ServiceError.NotFount);
        }
    }
}
