using Moq;
using Microsoft.AspNetCore.Mvc;
using Caixaverso.Backend.API.Controllers.v1;
using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.API.Responses;

namespace Caixaverso.Backend.Tests.API.Controllers
{
    public class ProdutoControllerTests
    {
        private readonly ProdutoController _controller;
        private readonly Mock<IProdutoListarTodosUseCase> _listarMock = new();
        private readonly Mock<IProdutoCriarUseCase> _criarMock = new();
        private readonly Mock<IProdutoObterPorIdUseCase> _obterMock = new();
        private readonly Mock<IProdutoAtualizarUseCase> _atualizarMock = new();
        private readonly Mock<IProdutoDeletarUseCase> _deletarMock = new();
        private readonly Mock<ISimulacaoEmprestimoUseCase> _simularMock = new();

        public ProdutoControllerTests()
        {
            _controller = new ProdutoController(
                _listarMock.Object,
                _criarMock.Object,
                _obterMock.Object,
                _atualizarMock.Object,
                _deletarMock.Object,
                _simularMock.Object
            );
        }

        [Fact]
        public async Task GetTodos_DeveRetornarListaDeProdutos()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Crédito Pessoal", TaxaJurosAnual = 12.5, PrazoMaximoMeses = 24 },
                new Produto { Id = 2, Nome = "Crédito Consignado", TaxaJurosAnual = 9.8, PrazoMaximoMeses = 48 }
            };

            _listarMock.Setup(x => x.Handle()).ReturnsAsync(produtos);

            var result = await _controller.GetTodos();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(produtos, ok.Value);
        }

        [Fact]
        public async Task ObterPorId_ProdutoExiste_DeveRetornarProduto()
        {
            var produto = new Produto { Id = 1, Nome = "Crédito Pessoal", TaxaJurosAnual = 12.5, PrazoMaximoMeses = 24 };
            _obterMock.Setup(x => x.Handle(1)).ReturnsAsync(produto);

            var result = await _controller.ObterPorId(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(produto, ok.Value);
        }

        [Fact]
        public async Task ObterPorId_ProdutoNaoExiste_DeveRetornarNotFound()
        {
            _obterMock.Setup(x => x.Handle(99)).ReturnsAsync((Produto)null!);

            var result = await _controller.ObterPorId(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Criar_DeveRetornarCreatedAtActionComProduto()
        {
            var request = new CriarProdutoRequest
            {
                Nome = "Crédito Imobiliário",
                TaxaJurosAnual = 7.5,
                PrazoMaximoMeses = 360
            };

            var produtoCriado = new Produto
            {
                Id = 10,
                Nome = request.Nome,
                TaxaJurosAnual = request.TaxaJurosAnual,
                PrazoMaximoMeses = request.PrazoMaximoMeses
            };

            _criarMock.Setup(x => x.Handle(request)).ReturnsAsync(produtoCriado);

            var result = await _controller.Criar(request);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(produtoCriado, created.Value);
            Assert.Equal("ObterPorId", created.ActionName);
        }

        [Fact]
        public async Task Atualizar_ProdutoExiste_DeveRetornarProdutoAtualizado()
        {
            var request = new CriarProdutoRequest
            {
                Nome = "Crédito Atualizado",
                TaxaJurosAnual = 10.0,
                PrazoMaximoMeses = 36
            };

            var produtoAtualizado = new Produto
            {
                Id = 1,
                Nome = request.Nome,
                TaxaJurosAnual = request.TaxaJurosAnual,
                PrazoMaximoMeses = request.PrazoMaximoMeses
            };

            _atualizarMock.Setup(x => x.Handle(1, request)).ReturnsAsync(produtoAtualizado);

            var result = await _controller.Atualizar(1, request);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(produtoAtualizado, ok.Value);
        }

        [Fact]
        public async Task Atualizar_ProdutoNaoExiste_DeveRetornarNotFound()
        {
            var request = new CriarProdutoRequest
            {
                Nome = "Crédito Atualizado",
                TaxaJurosAnual = 10.0,
                PrazoMaximoMeses = 36
            };

            _atualizarMock.Setup(x => x.Handle(1, request)).ReturnsAsync((Produto)null!);

            var result = await _controller.Atualizar(1, request);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Remover_DeveRetornarNoContent()
        {
            _deletarMock.Setup(x => x.Handle(1)).Returns(Task.CompletedTask);

            var result = await _controller.Remover(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task SimularEmprestimo_DeveRetornarResultadoCompleto()
        {
            // Arrange
            var produto = new Produto
            {
                Id = 1,
                Nome = "Crédito Pessoal",
                TaxaJurosAnual = 12.0,
                PrazoMaximoMeses = 24
            };

            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = produto.Id,
                ValorSolicitado = 10000,
                PrazoMeses = 12
            };

            var responseEsperada = SimulacaoEmprestimoResponse.Calcular(produto, request.ValorSolicitado, request.PrazoMeses);

            _simularMock.Setup(x => x.Handle(request)).ReturnsAsync(responseEsperada);

            // Act
            var result = await _controller.SimularEmprestimo(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SimulacaoEmprestimoResponse>(okResult.Value);

            Assert.Equal(produto.Id, response.Produto.Id);
            Assert.Equal(produto.Nome, response.Produto.Nome);
            Assert.Equal(produto.TaxaJurosAnual, response.Produto.TaxaJurosAnual);
            Assert.Equal(produto.PrazoMaximoMeses, response.Produto.PrazoMaximoMeses);

            Assert.Equal(request.ValorSolicitado, response.ValorSolicitado);
            Assert.Equal(request.PrazoMeses, response.PrazoMeses);
            Assert.Equal(responseEsperada.ValorTotalComJuros, response.ValorTotalComJuros);
            Assert.Equal(responseEsperada.ParcelaMensal, response.ParcelaMensal);
            Assert.Equal(responseEsperada.TaxaJurosEfetivaMensal, response.TaxaJurosEfetivaMensal);

            Assert.NotEmpty(response.MemoriaCalculo);
            Assert.Equal(12, response.MemoriaCalculo.Count);
        }


        [Fact]
        public async Task SimularEmprestimo_ProdutoNaoEncontrado_DeveRetornarNotFound()
        {
            var request = new SimulacaoEmprestimoRequest
            {
                IdProduto = 99,
                ValorSolicitado = 10000,
                PrazoMeses = 12
            };

            _simularMock.Setup(x => x.Handle(request)).ReturnsAsync((SimulacaoEmprestimoResponse)null!);

            var result = await _controller.SimularEmprestimo(request);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
