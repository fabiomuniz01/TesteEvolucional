
using FluentValidation;

namespace Evolucional.Application.Alunos.Queries.GetAlunosComPaginacao
{
    public class GetAllAlunosComPaginacaoQueryValidator : AbstractValidator<GetAllAlunosComPaginacaoQuery>
    {
        public GetAllAlunosComPaginacaoQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Número da página pelo menos maior ou igual a 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Tamanho da página pelo menos maior ou igual a 1.");
        }
    }
}
