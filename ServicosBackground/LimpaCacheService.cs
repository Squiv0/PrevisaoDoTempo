using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Dados.Interfaces;

namespace ServicosBackground
{
    public class LimpaCacheService : BackgroundService
    {
        private readonly ILogger<LimpaCacheService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _intervalo = TimeSpan.FromMinutes(2);

        public LimpaCacheService(ILogger<LimpaCacheService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Serviço de limpeza de cache iniciado. Intervalo: {Intervalo}", _intervalo);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Iniciando limpeza do cache...");
                    await LimparCacheAsync();
                    _logger.LogInformation("Limpeza do cache concluída com sucesso.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro durante a limpeza do cache.");
                }

                _logger.LogInformation("Próxima limpeza em {Intervalo}", _intervalo);
                await Task.Delay(_intervalo, stoppingToken);
            }
        }

        private async Task LimparCacheAsync()
        {
            using var scope = _serviceProvider.CreateScope();

            var climaRepo = scope.ServiceProvider.GetRequiredService<IClimaRepository>();
            var previsaoRepo = scope.ServiceProvider.GetRequiredService<IPrevisaoRepository>();
            var historicoRepo = scope.ServiceProvider.GetRequiredService<IHistoricoRepository>();

            // Limpa todas as tabelas de cache
            await climaRepo.RemoverTodosAsync();
            await previsaoRepo.RemoverTodosAsync();
            await historicoRepo.RemoverTodosAsync();

            _logger.LogInformation("Cache limpo: Climas, Previsões e Históricos removidos.");
        }
    }
}