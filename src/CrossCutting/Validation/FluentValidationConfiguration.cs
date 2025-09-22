using FluentValidation.AspNetCore;
using Caixaverso.Backend.API.Validators;
using FluentValidation;

namespace Caixaverso.Backend.CrossCutting.Validation
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentValidationSetup(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CriarProdutoRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<SimulacaoEmprestimoRequestValidator>();
        }
    }
}
