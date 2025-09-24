using Dados.Entidades;

namespace Dados.Interfaces
{
    public interface IClimaRepository
    {
        Task AdicionarAsync(Clima clima);
        Task<Clima> ObterAsync(string cidade);
        Task RemoverTodosAsync();
    }
}
