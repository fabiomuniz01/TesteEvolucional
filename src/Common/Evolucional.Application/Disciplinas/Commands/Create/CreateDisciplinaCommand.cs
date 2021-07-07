using System;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Interfaces;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Dto;
using Evolucional.Domain.Entities;
using MapsterMapper;

namespace Evolucional.Application.Disciplinas.Commands.Create
{
    public class CreateDisciplinaCommand : IRequestWrapper<DisciplinaDto>
    {
        public string Nome { get; set; }
    }

    public class CreateDisciplinaCommandHandler : IRequestHandlerWrapper<CreateDisciplinaCommand, DisciplinaDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateDisciplinaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DisciplinaDto>> Handle(CreateDisciplinaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Disciplina
                {
                    Nome = request.Nome
                };

                await _context.Disciplinas.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                return ServiceResult.Success(_mapper.Map<DisciplinaDto>(entity));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }
    }
}
