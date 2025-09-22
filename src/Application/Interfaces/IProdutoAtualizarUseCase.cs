using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.Domain.Entities;

namespace Caixaverso.Backend.Application.Interfaces
{
    public interface IProdutoAtualizarUseCase
    {
        Task<Produto?> Handle(int id, CriarProdutoRequest request);
    }
}