using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.Domain.Entities;

namespace Caixaverso.Backend.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ListarTodosAsync();
        Task AdicionarAsync(Produto produto);
        Task<Produto?> FindAsync(int id);
        Task<Produto?> AtualizarAsync(Produto produto);
        Task DeletarAsync(int id);
    }

}