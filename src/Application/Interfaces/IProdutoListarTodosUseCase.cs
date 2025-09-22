using Caixaverso.Backend.Domain.Entities;

namespace Caixaverso.Backend.Application.Interfaces
{
    public interface IProdutoListarTodosUseCase
    {
        Task<List<Produto>> Handle();
    }
}