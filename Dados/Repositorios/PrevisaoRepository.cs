using Dados.Contexto;
using Dados.Entidades;
using Dados.Interfaces;

namespace Dados.Repositorios
{
    public class PrevisaoRepository : IPrevisaoRepository
    {
        private readonly PrevisaoDoTempoDbContext _contexto;

        public PrevisaoRepository(PrevisaoDoTempoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarAsync(List<Previsao> previsoes)
        {
            _contexto.Previsoes.AddRange(previsoes);

            await _contexto.SaveChangesAsync();
        }

        public async Task<List<Previsao>> ObterAsync(string cidade)
        {
            return await Task.FromResult(_contexto.Previsoes
                .Where(p => p.Cidade == cidade)
                .OrderBy(p => p.Data)
                .ToList());
        }

        public async Task RemoverTodosAsync()
        {
            var antigos = _contexto.Previsoes.ToList();

            _contexto.Previsoes.RemoveRange(antigos);

            await _contexto.SaveChangesAsync();
        }
    }
}
