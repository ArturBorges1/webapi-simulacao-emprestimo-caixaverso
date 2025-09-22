using Moq;
using Caixaverso.Backend.Application.UseCase;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Tests.Application.UseCases
{
    public class ProdutoListarTodosUseCaseTests
    {
        [Fact(DisplayName = "Deve retornar todos os produtos quando o repositório retornar dados válidos")]
        public async Task Handle_DeveRetornarListaDeProdutos_QuandoRepositorioRetornarDadosValidos()
        {
            // Arrange
            var produtosEsperados = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto A", TaxaJurosAnual = 18, PrazoMaximoMeses = 24 },
            new Produto { Id = 2, Nome = "Produto B", TaxaJurosAnual = 20, PrazoMaximoMeses = 48 }
        };

            var mockRepositorio = new Mock<IProdutoRepository>();
            mockRepositorio
                .Setup(repo => repo.ListarTodosAsync())
                .ReturnsAsync(produtosEsperados);

            var useCase = new ProdutoListarTodosUseCase(mockRepositorio.Object);

            // Act
            var resultado = await useCase.Handle();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(produtosEsperados.Count, resultado.Count);

            Assert.Collection(resultado,
                produto =>
                {
                    Assert.Equal(1, produto.Id);
                    Assert.Equal("Produto A", produto.Nome);
                    Assert.Equal(18, produto.TaxaJurosAnual);
                    Assert.Equal(24, produto.PrazoMaximoMeses);
                },
                produto =>
                {
                    Assert.Equal(2, produto.Id);
                    Assert.Equal("Produto B", produto.Nome);
                    Assert.Equal(20, produto.TaxaJurosAnual);
                    Assert.Equal(48, produto.PrazoMaximoMeses);
                });
        }
    }
}

