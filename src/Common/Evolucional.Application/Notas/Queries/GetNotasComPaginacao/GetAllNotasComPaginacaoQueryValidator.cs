using FluentValidation;

namespace Evolucional.Application.Notas.Queries.GetNotasComPaginacao
{
    public class GetAllNotasComPaginacaoQueryValidator : AbstractValidator<GetAllNotasComPaginacaoQuery>
    {
        public GetAllNotasComPaginacaoQueryValidator()
        {
            RuleFor(x=>x.AlunoId)
                .NotNull()
                .NotEmpty().WithMessage("AlunoId é necessário.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Número da página pelo menos maior ou igual a 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Tamanho da página pelo menos maior ou igual a 1.");
        }
    }
}
