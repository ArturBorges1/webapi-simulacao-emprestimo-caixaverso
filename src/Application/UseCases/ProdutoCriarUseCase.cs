using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Application.UseCase
{
    public class ProdutoCriarUseCase(IProdutoRepository repository) : IProdutoCriarUseCase
    {
        private readonly IProdutoRepository _repository = repository;

        public async Task<Produto?> Handle(CriarProdutoRequest request)
        {
            var produto = new Produto
            {
                Nome = request.Nome,
                TaxaJurosAnual = request.TaxaJurosAnual,
                PrazoMaximoMeses = request.PrazoMaximoMeses
            };
            await _repository.AdicionarAsync(produto);
            return produto;
        }
    }
}
