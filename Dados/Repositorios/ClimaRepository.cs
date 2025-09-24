using Dados.Contexto;
using Dados.Entidades;
using Dados.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dados.Repositorios
{
    public class ClimaRepository : IClimaRepository
    {
        private readonly PrevisaoDoTempoDbContext _contexto;

        public ClimaRepository(PrevisaoDoTempoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarAsync(Clima clima)
        {
            _contexto.Climas.Add(clima);

            await _contexto.SaveChangesAsync();
        }

        public async Task<Clima> ObterAsync(string cidade)
        {
            return await _contexto.Climas
                .Where(c => c.Cidade == cidade)
                .FirstAsync();
        }

        public async Task RemoverTodosAsync()
        {
            var antigos = _contexto.Climas.ToList();

            _contexto.Climas.RemoveRange(antigos);

            await _contexto.SaveChangesAsync();
        }
    }
}
