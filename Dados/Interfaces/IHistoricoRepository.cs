using Dados.Entidades;

namespace Dados.Interfaces
{
    public interface IHistoricoRepository
    {
        Task AdicionarAsync(Historico historico);
        Task<List<Historico>> ListarAsync();
        Task RemoverTodosAsync();
    }
}
