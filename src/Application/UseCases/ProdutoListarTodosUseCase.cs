using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Application.UseCase
{
    public class ProdutoListarTodosUseCase(IProdutoRepository repository) : IProdutoListarTodosUseCase
    {
        private readonly IProdutoRepository _repository = repository;
        public async Task<List<Produto>> Handle()
        {
            return await _repository.ListarTodosAsync();
        }
    }
}