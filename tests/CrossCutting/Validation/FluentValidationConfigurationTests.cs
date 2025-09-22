using Xunit;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Caixaverso.Backend.CrossCutting.Validation;
using Caixaverso.Backend.API.Validators;
using FluentValidation;
using System.Linq;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.Tests.CrossCutting.Validation
{
    public class FluentValidationConfigurationTests
    {
        [Fact]
        public void AddFluentValidationSetup_ShouldRegisterExpectedValidators()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddFluentValidationSetup();

            // Assert
            var provider = services.BuildServiceProvider();

            // Verifica se os validadores foram registrados
            var criarProdutoValidator = provider.GetService<IValidator<CriarProdutoRequest>>();
            var simulacaoEmprestimoValidator = provider.GetService<IValidator<SimulacaoEmprestimoRequest>>();

            Assert.NotNull(criarProdutoValidator);
            Assert.NotNull(simulacaoEmprestimoValidator);
        }
    }
}
