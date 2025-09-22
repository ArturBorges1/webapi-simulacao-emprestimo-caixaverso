using Moq;
using Caixaverso.Backend.Application.UseCase;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Tests.Application.UseCases
{
    public class ProdutoObterPorIdUseCaseTests
    {
        [Fact(DisplayName = "Deve retornar o produto correto quando o ID existir no repositório")]
        public async Task Handle_DeveRetornarProduto_QuandoIdExistir()
        {
            // Arrange
            var produtoEsperado = new Produto
            {
                Id = 1,
                Nome = "Produto Teste",
                TaxaJurosAnual = 15,
                PrazoMaximoMeses = 36
            };

            var mockRepositorio = new Mock<IProdutoRepository>();
            mockRepositorio
                .Setup(repo => repo.FindAsync(1))
                .ReturnsAsync(produtoEsperado);

            var useCase = new ProdutoObterPorIdUseCase(mockRepositorio.Object);

            // Act
            var produtoRetornado = await useCase.Handle(1);

            // Assert
            mockRepositorio.Verify(repo => repo.FindAsync(1), Times.Once);

            Assert.NotNull(produtoRetornado);
            Assert.Equal(produtoEsperado.Id, produtoRetornado.Id);
            Assert.Equal(produtoEsperado.Nome, produtoRetornado.Nome);
            Assert.Equal(produtoEsperado.TaxaJurosAnual, produtoRetornado.TaxaJurosAnual);
            Assert.Equal(produtoEsperado.PrazoMaximoMeses, produtoRetornado.PrazoMaximoMeses);
        }
    }
}
