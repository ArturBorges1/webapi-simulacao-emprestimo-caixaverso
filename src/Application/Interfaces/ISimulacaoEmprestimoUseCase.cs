using Caixaverso.Backend.API.Requests;
using Caixaverso.Backend.API.Responses;

namespace Caixaverso.Backend.Application.Interfaces
{
    public interface ISimulacaoEmprestimoUseCase
    {
        Task<SimulacaoEmprestimoResponse?> Handle(SimulacaoEmprestimoRequest request);
    }
}