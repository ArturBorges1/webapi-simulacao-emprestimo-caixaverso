using FluentValidation;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.API.Validators
{
    public class CriarProdutoRequestValidator : AbstractValidator<CriarProdutoRequest>
    {
        public CriarProdutoRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.TaxaJurosAnual)
                .InclusiveBetween(0.1, 100).WithMessage("A taxa de juros deve estar entre 0.1% e 100%.");

            RuleFor(x => x.PrazoMaximoMeses)
                .InclusiveBetween(1, 120).WithMessage("O prazo deve estar entre 1 e 120 meses.");
        }
    }
}
