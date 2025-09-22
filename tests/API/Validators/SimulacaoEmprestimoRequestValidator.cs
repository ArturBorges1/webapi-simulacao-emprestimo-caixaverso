using FluentValidation.TestHelper;
using Caixaverso.Backend.API.Validators;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.Tests.API.Validators
{
    public class SimulacaoEmprestimoRequestValidatorTests
    {
        private readonly SimulacaoEmprestimoRequestValidator _validator = new();

        [Fact(DisplayName = "Deve validar corretamente uma solicitação válida de empréstimo")]
        public void Validacao_DevePassarComDadosValidos()
        {
            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = 1,
                ValorSolicitado = 10000.00m,
                PrazoMeses = 24
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldNotHaveAnyValidationErrors();
        }

        [Theory(DisplayName = "Deve gerar erro quando IdProduto for zero ou negativo")]
        [InlineData(0)]
        [InlineData(-1)]
        public void IdProdutoInvalido_DeveGerarErro(int idProduto)
        {
            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = idProduto,
                ValorSolicitado = 10000.00m,
                PrazoMeses = 24
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldHaveValidationErrorFor(r => r.IdProduto)
                     .WithErrorMessage("O IdProduto deve ser maior que zero.");
        }

        [Theory(DisplayName = "Deve gerar erro quando ValorSolicitado for zero ou negativo")]
        [InlineData(0)]
        [InlineData(-500)]
        public void ValorSolicitadoInvalido_DeveGerarErro(decimal valor)
        {
            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = 1,
                ValorSolicitado = valor,
                PrazoMeses = 24
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldHaveValidationErrorFor(r => r.ValorSolicitado)
                     .WithErrorMessage("O valor solicitado deve ser maior que zero.");
        }

        [Theory(DisplayName = "Deve gerar erro quando PrazoMeses estiver fora do intervalo permitido")]
        [InlineData(0)]
        [InlineData(121)]
        public void PrazoMesesForaDoIntervalo_DeveGerarErro(int prazo)
        {
            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = 1,
                ValorSolicitado = 10000.00m,
                PrazoMeses = prazo
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldHaveValidationErrorFor(r => r.PrazoMeses)
                     .WithErrorMessage("O prazo deve estar entre 1 e 120 meses.");
        }

        [Fact(DisplayName = "Deve passar na validação com valores mínimos válidos")]
        public void ValoresMinimosValidos_DevePassar()
        {
            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = 1,
                ValorSolicitado = 0.01m,
                PrazoMeses = 1
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Deve passar na validação com valores máximos válidos")]
        public void ValoresMaximosValidos_DevePassar()
        {
            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = int.MaxValue,
                ValorSolicitado = decimal.MaxValue,
                PrazoMeses = 120
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldNotHaveAnyValidationErrors();
        }
    }
}
