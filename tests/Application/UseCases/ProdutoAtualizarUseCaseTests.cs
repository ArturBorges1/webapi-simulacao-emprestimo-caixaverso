using Moq;
using Caixaverso.Backend.Application.UseCase;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;
using Caixaverso.Backend.API.Requests;

namespace Caixaverso.Backend.Tests.Application.UseCases
{
    public class ProdutoAtualizarUseCaseTests
    {
        [Fact(DisplayName = "Deve atualizar o produto corretamente quando o ID existir")]
        public async Task Handle_DeveAtualizarProduto_QuandoIdExistir()
        {
            // Arrange
            var idProduto = 1;

            var produtoExistente = new Produto
            {
                Id = idProduto,
                Nome = "Produto Antigo",
                TaxaJurosAnual = 10,
                PrazoMaximoMeses = 12
            };

            var request = new CriarProdutoRequest
            {
                Nome = "Produto Atualizado",
                TaxaJurosAnual = 20,
                PrazoMaximoMeses = 48
            };

            var produtoAtualizado = new Produto
            {
                Id = idProduto,
                Nome = request.Nome,
                TaxaJurosAnual = request.TaxaJurosAnual,
                PrazoMaximoMeses = request.PrazoMaximoMeses
            };

            var mockRepositorio = new Mock<IProdutoRepository>();
            mockRepositorio
                .Setup(repo => repo.FindAsync(idProduto))
                .ReturnsAsync(produtoExistente);

            mockRepositorio
                .Setup(repo => repo.AtualizarAsync(It.IsAny<Produto>()))
                .ReturnsAsync(produtoAtualizado);

            var useCase = new ProdutoAtualizarUseCase(mockRepositorio.Object);

            // Act
            var resultado = await useCase.Handle(idProduto, request);

            // Assert
            mockRepositorio.Verify(repo => repo.FindAsync(idProduto), Times.Once);
            mockRepositorio.Verify(repo => repo.AtualizarAsync(It.Is<Produto>(
                p => p.Id == idProduto &&
                     p.Nome == request.Nome &&
                     p.TaxaJurosAnual == request.TaxaJurosAnual &&
                     p.PrazoMaximoMeses == request.PrazoMaximoMeses
            )), Times.Once);

            Assert.NotNull(resultado);
            Assert.Equal(produtoAtualizado.Id, resultado.Id);
            Assert.Equal(produtoAtualizado.Nome, resultado.Nome);
            Assert.Equal(produtoAtualizado.TaxaJurosAnual, resultado.TaxaJurosAnual);
            Assert.Equal(produtoAtualizado.PrazoMaximoMeses, resultado.PrazoMaximoMeses);
        }
    }
}
