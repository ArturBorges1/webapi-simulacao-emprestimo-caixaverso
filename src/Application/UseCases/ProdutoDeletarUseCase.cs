using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Application.UseCase
{
    public class ProdutoDeletarUseCase(IProdutoRepository repository) : IProdutoDeletarUseCase
    {
        private readonly IProdutoRepository _repository = repository;

        public async Task Handle(int id)
        {
            await _repository.DeletarAsync(id);
        }
    }
}
