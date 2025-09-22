using Moq;
using Caixaverso.Backend.Application.UseCase;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.Tests.Application.UseCases
{
    public class SimulacaoEmprestimoUseCaseTests
    {
        [Fact(DisplayName = "Deve retornar a simulação com dados calculados corretamente quando o produto existir")]
        public async Task Handle_DeveRetornarSimulacao_QuandoProdutoExistir()
        {
            // Arrange
            var produto = new Produto
            {
                Id = 1,
                Nome = "Produto Teste",
                TaxaJurosAnual = 18,
                PrazoMaximoMeses = 48
            };

            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = produto.Id,
                ValorSolicitado = 10000m,
                PrazoMeses = 24
            };

            var mockRepositorio = new Mock<IProdutoRepository>();
            mockRepositorio
                .Setup(repo => repo.FindAsync(request.IdProduto))
                .ReturnsAsync(produto);

            var useCase = new SimulacaoEmprestimoUseCase(mockRepositorio.Object);

            // Act
            var resultado = await useCase.Handle(request);

            // Assert
            mockRepositorio.Verify(repo => repo.FindAsync(request.IdProduto), Times.Once);

            Assert.NotNull(resultado);
            Assert.Equal(produto, resultado.Produto);
            Assert.Equal(request.ValorSolicitado, resultado.ValorSolicitado);
            Assert.Equal(request.PrazoMeses, resultado.PrazoMeses);
            Assert.True(resultado.TaxaJurosEfetivaMensal > 0);
            Assert.True(resultado.ValorTotalComJuros > 0);
            Assert.True(resultado.ParcelaMensal > 0);
            Assert.NotNull(resultado.MemoriaCalculo);
            Assert.Equal(request.PrazoMeses, resultado.MemoriaCalculo.Count);

            var primeiraParcela = resultado.MemoriaCalculo[0];
            Assert.Equal(1, primeiraParcela.Mes);
            Assert.True(primeiraParcela.SaldoDevedorInicial > 0);
            Assert.True(primeiraParcela.Juros > 0);
            Assert.True(primeiraParcela.Amortizacao > 0);
            Assert.True(primeiraParcela.ValorParcela > 0);
            Assert.True(primeiraParcela.SaldoDevedorFinal > 0);
        }
    }
}
