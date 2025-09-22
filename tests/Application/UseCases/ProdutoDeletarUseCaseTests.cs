using Moq;
using Caixaverso.Backend.Application.UseCase;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Tests.Application.UseCases
{
    public class ProdutoDeletarUseCaseTests
    {
        [Fact(DisplayName = "Deve chamar o repositório para deletar o produto com o ID fornecido")]
        public async Task Handle_DeveChamarRepositorioParaDeletarProduto_QuandoIdForValido()
        {
            // Arrange
            var idProduto = 1;

            var mockRepositorio = new Mock<IProdutoRepository>();
            mockRepositorio
                .Setup(repo => repo.DeletarAsync(idProduto))
                .Returns(Task.CompletedTask);

            var useCase = new ProdutoDeletarUseCase(mockRepositorio.Object);

            // Act
            await useCase.Handle(idProduto);

            // Assert
            mockRepositorio.Verify(repo => repo.DeletarAsync(idProduto), Times.Once);
        }
    }
}
