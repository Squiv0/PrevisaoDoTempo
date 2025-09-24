using Dados.Contexto;
using Dados.Entidades;
using Dados.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dados.Repositorios
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly PrevisaoDoTempoDbContext _contexto;

        public HistoricoRepository(PrevisaoDoTempoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarAsync(Historico historico)
        {
            _contexto.Historicos.Add(historico);

            await _contexto.SaveChangesAsync();
        }

        public async Task<List<Historico>> ListarAsync()
        {
            return await _contexto.Historicos
                .OrderByDescending(h => h.DataConsulta)
                .ToListAsync();
        }

        public async Task RemoverTodosAsync()
        {
            var antigos = _contexto.Historicos.ToList();

            _contexto.Historicos.RemoveRange(antigos);

            await _contexto.SaveChangesAsync();
        }
    }
}
