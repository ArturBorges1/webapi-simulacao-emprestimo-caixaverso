using FluentValidation;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.API.Validators
{
    public class SimulacaoEmprestimoRequestValidator : AbstractValidator<SimulacaoEmprestimoRequest>
    {
        public SimulacaoEmprestimoRequestValidator()
        {
            RuleFor(x => x.IdProduto)
                .NotEmpty().WithMessage("O campo IdProduto é obrigatório.")
                .GreaterThan(0).WithMessage("O IdProduto deve ser maior que zero.");

            RuleFor(x => x.ValorSolicitado)
                .NotEmpty().WithMessage("O campo ValorSolicitado é obrigatório.")
                .GreaterThan(0).WithMessage("O valor solicitado deve ser maior que zero.");

            RuleFor(x => x.PrazoMeses)
                .NotEmpty().WithMessage("O campo PrazoMeses é obrigatório.")
                .InclusiveBetween(1, 120).WithMessage("O prazo deve estar entre 1 e 120 meses.");
        }
    }
}
