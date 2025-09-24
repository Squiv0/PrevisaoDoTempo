using Dados.Entidades;
using Dados.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Servicos.Configuracoes;
using Servicos.DTOs;
using Servicos.Extensions;
using Servicos.Interfaces;
using Servicos.Utils;

namespace Servicos.Servicos
{
    public class PrevisaoService : IPrevisaoService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiConfiguracao _weatherApiConfig;
        private readonly IPrevisaoRepository _previsaoRepository;
        private readonly IHistoricoService _historicoService;

        public PrevisaoService(IOptions<WeatherApiConfiguracao> config, IPrevisaoRepository previsaoRepository, IHistoricoService historicoService)
        {
            _httpClient = new HttpClient();
            _weatherApiConfig = config.Value;
            _previsaoRepository = previsaoRepository;
            _historicoService = historicoService;
        }

        public async Task<List<PrevisaoResponseDto>> ObterPrevisao5DiasAsync(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new ArgumentException("Cidade não pode ser nula ou vazia!", nameof(cidade));

            List<Previsao> previsoes;

            if (await _historicoService.ValidarExistenciaHistorico(cidade, TipoHistorico.PREVISAO))
                previsoes = await _previsaoRepository.ObterAsync(cidade);
            else
                previsoes = await ConsultarPrevisoesAPIAsync(cidade);

            return previsoes.ToPrevisaoResponse();
        }

        private async Task<List<Previsao>> ConsultarPrevisoesAPIAsync(string cidade)
        {
            var url = $"{_weatherApiConfig.BaseUrl}/forecast.json?key={_weatherApiConfig.Chave}&q={cidade}&days=5&lang=pt";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var erro = JsonConvert.DeserializeObject<ErroApiRespostaDto>(content);
                throw new HttpRequestException($"Erro ao consultar clima: {erro?.Erro.Codigo} - {erro?.Erro.Mensagem}.");
            }

            var resultado = await response.Content.ReadAsStringAsync();
            var previsaoDto = JsonConvert.DeserializeObject<PrevisaoDto>(resultado) ?? throw new Exception("Erro ao desserializar a resposta da previsão.");
            var previsoes = previsaoDto.ToEntityList();

            await AdicionarPrevisoesAsync(previsoes);

            return previsoes;
        }

        private async Task AdicionarPrevisoesAsync(List<Previsao> previsoes)
        {
            await _previsaoRepository.AdicionarAsync(previsoes);
            await _historicoService.AdicionarHistoricoAsync(previsoes.First().Cidade, TipoHistorico.PREVISAO);
        }
    }
}
