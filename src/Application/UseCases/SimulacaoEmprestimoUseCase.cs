using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.API.Responses;
using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Domain.Interfaces;

namespace Caixaverso.Backend.Application.UseCase
{
    public class SimulacaoEmprestimoUseCase(IProdutoRepository repository) : ISimulacaoEmprestimoUseCase
    {
        private readonly IProdutoRepository _repository = repository;
        public async Task<SimulacaoEmprestimoResponse?> Handle(SimulacaoEmprestimoRequest request)
        {
            var produtoEscolhido = await _repository.FindAsync(request.IdProduto);
            if (produtoEscolhido == null) return null;
            return SimulacaoEmprestimoResponse.Calcular(produtoEscolhido, request.ValorSolicitado, request.PrazoMeses);
        }
    }
}