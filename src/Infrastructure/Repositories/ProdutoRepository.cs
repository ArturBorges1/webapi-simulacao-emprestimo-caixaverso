using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;
using Caixaverso.Backend.Infrastructure.Sqlite.Context;
using Microsoft.EntityFrameworkCore;

namespace Caixaverso.Backend.Infrastructure.Repositories
{
    public class ProdutoRepository(SqliteContext context) : IProdutoRepository
    {
        private readonly SqliteContext _context = context;

        public async Task<List<Produto>> ListarTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto?> FindAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<Produto?> AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task DeletarAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
