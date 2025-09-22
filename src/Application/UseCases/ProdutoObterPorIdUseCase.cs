using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Application.UseCase
{
    public class ProdutoObterPorIdUseCase(IProdutoRepository repository) : IProdutoObterPorIdUseCase
    {
        private readonly IProdutoRepository _repository = repository;
        public async Task<Produto?> Handle(int id)
        {
            return await _repository.FindAsync(id);
        }
    }
}