using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.Domain.Entities;

namespace Caixaverso.Backend.Application.Interfaces
{
    public interface IProdutoCriarUseCase
    {
        Task<Produto?> Handle(CriarProdutoRequest request);
    }
}