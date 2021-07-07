using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Interfaces;
using Evolucional.Application.Common.Mapping;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Dto;
using Mapster;
using MapsterMapper;

namespace Evolucional.Application.Notas.Queries.GetNotasComPaginacao
{
    public class GetAllNotasComPaginacaoQuery : IRequestWrapper<PaginatedList<NotaDto>>
    {
        public int DisciplinaId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllVillagesWithPaginationQueryHandler : IRequestHandlerWrapper<GetAllNotasComPaginacaoQuery, PaginatedList<NotaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllVillagesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<NotaDto>>> Handle(GetAllNotasComPaginacaoQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<NotaDto> list = await _context.Notas
                .Where(x => x.DisciplinaId == request.DisciplinaId)
                .OrderBy(o => o.Valor)
                .ProjectToType<NotaDto>(_mapper.Config)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<NotaDto>>(ServiceError.NotFount);
        }
    }
}
