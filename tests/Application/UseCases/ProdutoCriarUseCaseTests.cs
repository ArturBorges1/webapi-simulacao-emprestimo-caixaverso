using Moq;
using Caixaverso.Backend.Application.UseCase;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.Tests.Application.UseCases
{
    public class ProdutoCriarUseCaseTests
    {
        [Fact(DisplayName = "Deve criar um produto corretamente e chamar o repositório uma vez")]
        public async Task Handle_DeveCriarProduto_QuandoRequestForValido()
        {
            // Arrange
            var request = new CriarProdutoRequest
            {
                Nome = "Produto Teste",
                TaxaJurosAnual = 15,
                PrazoMaximoMeses = 36
            };

            var mockRepositorio = new Mock<IProdutoRepository>();
            mockRepositorio
                .Setup(repo => repo.AdicionarAsync(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);

            var useCase = new ProdutoCriarUseCase(mockRepositorio.Object);

            // Act
            var produtoCriado = await useCase.Handle(request);

            // Assert
            mockRepositorio.Verify(repo => repo.AdicionarAsync(It.Is<Produto>(
                p => p.Nome == request.Nome &&
                     p.TaxaJurosAnual == request.TaxaJurosAnual &&
                     p.PrazoMaximoMeses == request.PrazoMaximoMeses
            )), Times.Once);

            Assert.NotNull(produtoCriado);
            Assert.Equal(request.Nome, produtoCriado.Nome);
            Assert.Equal(request.TaxaJurosAnual, produtoCriado.TaxaJurosAnual);
            Assert.Equal(request.PrazoMaximoMeses, produtoCriado.PrazoMaximoMeses);
        }
    }
}
