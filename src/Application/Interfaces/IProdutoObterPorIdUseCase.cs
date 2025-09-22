using Caixaverso.Backend.Domain.Entities;

namespace Caixaverso.Backend.Application.Interfaces
{
    public interface IProdutoObterPorIdUseCase
    {
        Task<Produto?> Handle(int id);
    }
}