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

namespace Evolucional.Application.Alunos.Queries.GetAlunosComPaginacao
{
    public class GetAllAlunosComPaginacaoQuery : IRequestWrapper<PaginatedList<AlunoDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

    public class GetCitiesQueryHandler : IRequestHandlerWrapper<GetAllAlunosComPaginacaoQuery, PaginatedList<AlunoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<AlunoDto>>> Handle(GetAllAlunosComPaginacaoQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<AlunoDto> list = await _context.Alunos
                .ProjectToType<AlunoDto>(_mapper.Config)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<AlunoDto>>(ServiceError.NotFount);
        }
    }
}
