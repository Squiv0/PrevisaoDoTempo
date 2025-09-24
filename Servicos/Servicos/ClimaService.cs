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
    public class ClimaService : IClimaService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiConfiguracao _weatherApiConfig;
        private readonly IClimaRepository _climaRepository;
        private readonly IHistoricoService _historicoService;

        public ClimaService(IOptions<WeatherApiConfiguracao> config, IClimaRepository climaRepository, IHistoricoService historicoService)
        {
            _httpClient = new HttpClient();
            _weatherApiConfig = config.Value;
            _climaRepository = climaRepository;
            _historicoService = historicoService;
        }

        public async Task<ClimaResponseDto> ObterClimaAsync(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new ArgumentException("Cidade não pode ser nula ou vazia!", nameof(cidade));

            Clima clima;

            if (await _historicoService.ValidarExistenciaHistorico(cidade, TipoHistorico.CLIMA))
                clima = await _climaRepository.ObterAsync(cidade);
            else
                clima = await ConsultarClimaAPIAsync(cidade);

            return clima.ToClimaResponse();
        }

        private async Task<Clima> ConsultarClimaAPIAsync(string cidade)
        {
            var url = $"{_weatherApiConfig.BaseUrl}/current.json?key={_weatherApiConfig.Chave}&q={cidade}&lang=pt";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var erro = JsonConvert.DeserializeObject<ErroApiRespostaDto>(content);
                throw new HttpRequestException($"Erro ao consultar clima: {erro?.Erro.Codigo} - {erro?.Erro.Mensagem}.");
            }

            var resultado = await response.Content.ReadAsStringAsync();
            var climaDto = JsonConvert.DeserializeObject<ClimaDto>(resultado) ?? throw new Exception("Erro ao desserializar a resposta do clima.");
            var clima = climaDto.ToEntity();

            await AdicionarClimaAsync(clima);

            return clima;
        }

        private async Task AdicionarClimaAsync(Clima clima)
        {
            await _climaRepository.AdicionarAsync(clima);
            await _historicoService.AdicionarHistoricoAsync(clima.Cidade, TipoHistorico.CLIMA);
        }
    }
}
