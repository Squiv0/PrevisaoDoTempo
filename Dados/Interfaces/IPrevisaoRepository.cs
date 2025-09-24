using Dados.Entidades;

namespace Dados.Interfaces
{
    public interface IPrevisaoRepository
    {
        Task AdicionarAsync(List<Previsao> previsoes);
        Task<List<Previsao>> ObterAsync(string cidade);
        Task RemoverTodosAsync();
    }
}
