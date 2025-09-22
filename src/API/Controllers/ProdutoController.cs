using Microsoft.AspNetCore.Mvc;
using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.API.Responses;

namespace Caixaverso.Backend.API.Controllers.v1
{
    /// <summary>
    /// Controller responsável por operações relacionadas aos produtos de empréstimo.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/simulacao-emprestimo/v{version:apiVersion}/produto")]
    [Produces("application/json")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoListarTodosUseCase _produtoListarUseCase;
        private readonly IProdutoCriarUseCase _produtoCriarUseCase;
        private readonly IProdutoObterPorIdUseCase _produtoObterPorIdUseCase;
        private readonly IProdutoAtualizarUseCase _produtoAtualizarUseCase;
        private readonly IProdutoDeletarUseCase _produtoDeletarUseCase;
        private readonly ISimulacaoEmprestimoUseCase _simulacaoEmprestimoUseCase;


        public ProdutoController(
            IProdutoListarTodosUseCase produtoListarUseCase,
            IProdutoCriarUseCase produtoCriarUseCase,
            IProdutoObterPorIdUseCase produtoObterPorIdUseCase,
            IProdutoAtualizarUseCase produtoAtualizarUseCase,
            IProdutoDeletarUseCase produtoDeletarUseCase,
            ISimulacaoEmprestimoUseCase simulacaoEmprestimoUseCase
        )
        {
            _produtoListarUseCase = produtoListarUseCase;
            _produtoCriarUseCase = produtoCriarUseCase;
            _produtoObterPorIdUseCase = produtoObterPorIdUseCase;
            _produtoAtualizarUseCase = produtoAtualizarUseCase;
            _produtoDeletarUseCase = produtoDeletarUseCase;
            _simulacaoEmprestimoUseCase = simulacaoEmprestimoUseCase;
        }
        /// <summary>
        /// Retorna todos os produtos de empréstimo disponíveis.
        /// </summary>
        /// <returns>Lista de produtos com informações como nome, taxa de juros anual e prazo máximo.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Produto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodos()
        {
            var produtos = await _produtoListarUseCase.Handle();
            return Ok(produtos);
        }

        /// <summary>
        /// Retorna um produto específico com base no ID informado.
        /// </summary>
        /// <param name="id">ID do produto a ser consultado.</param>
        /// <returns>Informações do produto correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var produto = await _produtoObterPorIdUseCase.Handle(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        /// <summary>
        /// Cria um novo produto de empréstimo.
        /// </summary>
        /// <param name="produto">Objeto contendo os dados do produto a ser criado.</param>
        /// <returns>Produto criado com o respectivo local de acesso.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] CriarProdutoRequest produto)
        {
            var produtoCriado = await _produtoCriarUseCase.Handle(produto);
            return CreatedAtAction(nameof(ObterPorId), new { produtoCriado?.Id }, produtoCriado);
        }


        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="produto">Objeto contendo os novos dados do produto.</param>
        /// <returns>Resposta sem conteúdo indicando sucesso na operação.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CriarProdutoRequest produto)
        {
            var produtoEditado = await _produtoAtualizarUseCase.Handle(id, produto);
            if (produtoEditado == null) return NotFound();
            return Ok(produtoEditado);
        }

        /// <summary>
        /// Remove um produto de empréstimo com base no ID informado.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>Resposta sem conteúdo indicando sucesso na remoção.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Remover(int id)
        {
            await _produtoDeletarUseCase.Handle(id);
            return NoContent();
        }

        /// <summary>
        /// Efetua a simulação do empréstimo
        /// </summary>
        /// <param name="simulacao">Objeto contendo o produto, valor solicitado e o prazo para o empréstimo.</param>
        /// <returns>Cálculo da simulação do empréstimo do produto informado.</returns>
        [HttpPost("simular-emprestimo")]
        [ProducesResponseType(typeof(SimulacaoEmprestimoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SimularEmprestimo(SimulacaoEmprestimoRequest simulacao)
        {
            var calculo = await _simulacaoEmprestimoUseCase.Handle(simulacao);
            if (calculo == null) return NotFound();
            return Ok(calculo);
        }
    }
}
