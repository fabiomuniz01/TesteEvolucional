using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Interfaces;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Dto;
using Evolucional.Domain.Entities;
using MapsterMapper;

namespace Evolucional.Application.Alunos.Commands.Create
{
    public class CreateAlunoCommand : IRequestWrapper<AlunoDto>
    {
        public string Nome { get; set; }
    }

    public class CreateAlunoCommandHandler : IRequestHandlerWrapper<CreateAlunoCommand, AlunoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAlunoCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AlunoDto>> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Aluno
            {
                Nome = request.Nome
            };

            await _context.Alunos.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<AlunoDto>(entity));
        }
    }
}
