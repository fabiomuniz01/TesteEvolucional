
using FluentValidation;

namespace Evolucional.Application.Disciplinas.Queries.GetDisciplinasComPaginacao
{
    public class GetAllDisciplinasComPaginacaoQueryValidator : AbstractValidator<GetAllDisciplinasComPaginacaoQuery>
    {
        public GetAllDisciplinasComPaginacaoQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Número da página pelo menos maior ou igual a 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Tamanho da página pelo menos maior ou igual a 1.");
        }
    }
}
