using Dados.Entidades;
using Dados.Interfaces;
using Servicos.DTOs;
using Servicos.Extensions;
using Servicos.Interfaces;
using Servicos.Utils;

namespace Servicos.Servicos
{
    public class HistoricoService : IHistoricoService
    {
        private readonly IHistoricoRepository _historicoRepository;

        public HistoricoService(IHistoricoRepository historicoRepository)
        {
            _historicoRepository = historicoRepository;
        }

        public async Task<List<HistoricoResponseDto>> ExibirHistoricoAsync()
        {
            var historico = await ObterHistoricoAsync();

            return historico.ToHistoricoResponse();
        }

        public async Task<bool> ValidarExistenciaHistorico(string cidade, TipoHistorico tipoHistorico)
        {
            var historicos = await ObterHistoricoAsync();

            if (tipoHistorico == TipoHistorico.CLIMA)
                return historicos.Any(h => h.Cidade.ToLower() == cidade.ToLower() && h.IsClima);
            else
                return historicos.Any(h => h.Cidade.ToLower() == cidade.ToLower() && h.IsPrevisao);
        }

        public async Task AdicionarHistoricoAsync(string cidade, TipoHistorico tipoHistorico)
        {
            var historico = new Historico
            {
                Cidade = cidade,
                DataConsulta = DateTime.UtcNow
            };

            switch (tipoHistorico)
            {
                case TipoHistorico.CLIMA:
                    historico.IsClima = true;
                    break;
                case TipoHistorico.PREVISAO:
                    historico.IsPrevisao = true;
                    break;
            }

            await _historicoRepository.AdicionarAsync(historico);
        }

        private async Task<List<Historico>> ObterHistoricoAsync()
        {
            return await _historicoRepository.ListarAsync();
        }

    }
}
