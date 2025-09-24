using Dados.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Dados.Contexto
{
    public class PrevisaoDoTempoDbContext : DbContext
    {
        public PrevisaoDoTempoDbContext(DbContextOptions<PrevisaoDoTempoDbContext> options) : base(options) { }

        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Previsao> Previsoes { get; set; }
        public DbSet<Clima> Climas { get; set; }
    }
}
