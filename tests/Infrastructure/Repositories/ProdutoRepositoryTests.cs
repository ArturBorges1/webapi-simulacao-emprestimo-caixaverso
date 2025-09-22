using Xunit;
using Microsoft.EntityFrameworkCore;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Infrastructure.Repositories;
using Caixaverso.Backend.Infrastructure.Sqlite.Context;
using System.Threading.Tasks;

namespace Caixaverso.Backend.Tests.Infrastructure.Repositories
{
    public class ProdutoRepositoryTests
    {
        private SqliteContext CriarContextoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<SqliteContext>()
                .UseInMemoryDatabase(databaseName: $"DbTeste_{Guid.NewGuid()}")
                .Options;

            return new SqliteContext(options);
        }

        private Produto CriarProdutoPadrao(int id, string nome, double taxa, int prazo)
        {
            return new Produto
            {
                Id = id,
                Nome = nome,
                TaxaJurosAnual = taxa,
                PrazoMaximoMeses = prazo
            };
        }

        [Fact(DisplayName = "Adicionar produto deve persistir no contexto")]
        public async Task AdicionarProduto_DevePersistirNoBanco()
        {
            var contexto = CriarContextoEmMemoria();
            var repositorio = new ProdutoRepository(contexto);

            var novoProduto = CriarProdutoPadrao(1, "Produto Teste", 12.5, 36);

            await repositorio.AdicionarAsync(novoProduto);

            var produtosPersistidos = await repositorio.ListarTodosAsync();

            Assert.Single(produtosPersistidos);
            Assert.Equal("Produto Teste", produtosPersistidos[0].Nome);
        }

        [Fact(DisplayName = "Listar todos deve retornar todos os produtos persistidos")]
        public async Task ListarTodos_DeveRetornarTodosProdutos()
        {
            var contexto = CriarContextoEmMemoria();
            var repositorio = new ProdutoRepository(contexto);

            await repositorio.AdicionarAsync(CriarProdutoPadrao(1, "Produto A", 10, 24));
            await repositorio.AdicionarAsync(CriarProdutoPadrao(2, "Produto B", 15, 48));

            var produtos = await repositorio.ListarTodosAsync();

            Assert.Equal(2, produtos.Count);
        }

        [Fact(DisplayName = "Buscar por ID deve retornar o produto correto")]
        public async Task BuscarPorId_DeveRetornarProdutoEsperado()
        {
            var contexto = CriarContextoEmMemoria();
            var repositorio = new ProdutoRepository(contexto);

            var produtoEsperado = CriarProdutoPadrao(3, "Produto C", 9.5, 12);
            await repositorio.AdicionarAsync(produtoEsperado);

            var produtoEncontrado = await repositorio.FindAsync(3);

            Assert.NotNull(produtoEncontrado);
            Assert.Equal("Produto C", produtoEncontrado?.Nome);
        }

        [Fact(DisplayName = "Atualizar produto deve refletir as mudanças")]
        public async Task AtualizarProduto_DevePersistirAlteracoes()
        {
            var contexto = CriarContextoEmMemoria();
            var repositorio = new ProdutoRepository(contexto);

            var produtoOriginal = CriarProdutoPadrao(4, "Produto D", 8, 18);
            await repositorio.AdicionarAsync(produtoOriginal);

            produtoOriginal.Nome = "Produto D Atualizado";
            produtoOriginal.TaxaJurosAnual = 11;

            var produtoAtualizado = await repositorio.AtualizarAsync(produtoOriginal);

            Assert.Equal("Produto D Atualizado", produtoAtualizado!.Nome);
            Assert.Equal(11, produtoAtualizado.TaxaJurosAnual);
        }

        [Fact(DisplayName = "Excluir produto deve removê-lo do contexto")]
        public async Task ExcluirProduto_DeveRemoverDoBanco()
        {
            var contexto = CriarContextoEmMemoria();
            var repositorio = new ProdutoRepository(contexto);

            var produtoParaExcluir = CriarProdutoPadrao(5, "Produto E", 7.5, 30);
            await repositorio.AdicionarAsync(produtoParaExcluir);

            await repositorio.DeletarAsync(5);

            var produtoRemovido = await repositorio.FindAsync(5);
            Assert.Null(produtoRemovido);
        }
    }
}
