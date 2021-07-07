using System;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Interfaces;
using Evolucional.Application.Common.Models;
using Evolucional.Application.Dto;
using Evolucional.Domain.Entities;
using MapsterMapper;

namespace Evolucional.Application.Notas.Commands.Create
{
    public class CreateNotaCommand : IRequestWrapper<NotaDto>
    {
        public decimal Valor { get; set; }
        public int DisciplinaId { get; set; }
        public int AlunoId { get; set; }
    }

    public class CreateNotaCommandHandler : IRequestHandlerWrapper<CreateNotaCommand, NotaDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateNotaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<NotaDto>> Handle(CreateNotaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Nota
                {
                    Valor = request.Valor,
                    DisciplinaId = request.DisciplinaId,
                    AlunoId = request.AlunoId
                };

                await _context.Notas.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success(_mapper.Map<NotaDto>(entity));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
