using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Evolucional.Application.Alunos.Commands.Create
{
    public class CreateAlunoCommandValidator : AbstractValidator<CreateAlunoCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateAlunoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .MaximumLength(100).WithMessage("O nome não deve ter mais de 100 caracteres.")
                .MustAsync(BeUniqueName).WithMessage("O aluno informaado já existe.")
                .NotEmpty().WithMessage("O nome é obrigatório.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Alunos.AllAsync(x => x.Nome != name, cancellationToken);
        }
    }
}
