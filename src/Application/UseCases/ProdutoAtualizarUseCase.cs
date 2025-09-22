using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Application.UseCase
{
    public class ProdutoAtualizarUseCase(IProdutoRepository repository) : IProdutoAtualizarUseCase
    {
        private readonly IProdutoRepository _repository = repository;

        public async Task<Produto?> Handle(int id, CriarProdutoRequest request)
        {
            var produtoExistente = await _repository.FindAsync(id);
            if (produtoExistente == null) return null;

            produtoExistente!.Id = id;
            produtoExistente.Nome = request.Nome;
            produtoExistente.TaxaJurosAnual = request.TaxaJurosAnual;
            produtoExistente.PrazoMaximoMeses = request.PrazoMaximoMeses;

            var produtoNovo = await _repository.AtualizarAsync(produtoExistente);
            return produtoNovo;
        }
    }
}
