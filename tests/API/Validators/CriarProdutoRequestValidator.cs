using FluentValidation.TestHelper;
using Caixaverso.Backend.API.Validators;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.Tests.API.Validators
{
    public class CriarProdutoRequestValidatorTests
    {
        private readonly CriarProdutoRequestValidator _validator = new();

        [Fact(DisplayName = "Validação deve passar com dados válidos")]
        public void Validacao_DevePassarComDadosValidos()
        {
            var request = new CriarProdutoRequest
            {
                Nome = "Produto Válido",
                TaxaJurosAnual = 10.5,
                PrazoMaximoMeses = 36
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Nome vazio deve gerar erro de validação")]
        public void NomeVazio_DeveGerarErro()
        {
            var request = new CriarProdutoRequest
            {
                Nome = "",
                TaxaJurosAnual = 10,
                PrazoMaximoMeses = 36
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldHaveValidationErrorFor(r => r.Nome)
                     .WithErrorMessage("O nome é obrigatório.");
        }

        [Fact(DisplayName = "Nome com mais de 100 caracteres deve gerar erro")]
        public void NomeMuitoLongo_DeveGerarErro()
        {
            var request = new CriarProdutoRequest
            {
                Nome = new string('A', 101),
                TaxaJurosAnual = 10,
                PrazoMaximoMeses = 36
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldHaveValidationErrorFor(r => r.Nome)
                     .WithErrorMessage("O nome deve ter no máximo 100 caracteres.");
        }

        [Theory(DisplayName = "Taxa de juros fora do intervalo deve gerar erro")]
        [InlineData(0.0)]
        [InlineData(100.1)]
        public void TaxaJurosForaDoIntervalo_DeveGerarErro(double taxa)
        {
            var request = new CriarProdutoRequest
            {
                Nome = "Produto",
                TaxaJurosAnual = taxa,
                PrazoMaximoMeses = 36
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldHaveValidationErrorFor(r => r.TaxaJurosAnual)
                     .WithErrorMessage("A taxa de juros deve estar entre 0.1% e 100%.");
        }

        [Theory(DisplayName = "Prazo fora do intervalo deve gerar erro")]
        [InlineData(0)]
        [InlineData(121)]
        public void PrazoForaDoIntervalo_DeveGerarErro(int prazo)
        {
            var request = new CriarProdutoRequest
            {
                Nome = "Produto",
                TaxaJurosAnual = 10,
                PrazoMaximoMeses = prazo
            };

            var resultado = _validator.TestValidate(request);
            resultado.ShouldHaveValidationErrorFor(r => r.PrazoMaximoMeses)
                     .WithErrorMessage("O prazo deve estar entre 1 e 120 meses.");
        }
    }
}
