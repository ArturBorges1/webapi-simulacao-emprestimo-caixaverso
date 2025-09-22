namespace Caixaverso.Backend.Application.Interfaces
{
    public interface IProdutoDeletarUseCase
    {
        Task Handle(int id);
    }
}