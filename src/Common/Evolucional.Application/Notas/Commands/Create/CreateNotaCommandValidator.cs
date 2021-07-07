using FluentValidation;

namespace Evolucional.Application.Notas.Commands.Create
{
    public class CreateNotaCommandValidator : AbstractValidator<CreateNotaCommand>
    {
        public CreateNotaCommandValidator()
        {
            RuleFor(v => v.Valor)
                .NotEmpty().WithMessage("O valor é obrigatório.");
        }
    }
}
