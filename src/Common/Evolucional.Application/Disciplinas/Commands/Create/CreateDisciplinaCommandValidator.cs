using FluentValidation;

namespace Evolucional.Application.Disciplinas.Commands.Create
{
    public class CreateDisciplinaCommandValidator : AbstractValidator<CreateDisciplinaCommand>
    {
        public CreateDisciplinaCommandValidator()
        {
            RuleFor(v => v.Nome)
                .MaximumLength(100).WithMessage("O nome não deve ter mais de 100 caracteres.")
                .NotEmpty().WithMessage("O nome é obrigatório.");
        }
    }
}
