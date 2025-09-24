

using Dados.Entidades;
using Servicos.DTOs;

namespace Servicos.Extensions
{
    public static class ResponseDtoExtensions
    {
        public static ClimaResponseDto ToClimaResponse(this Clima clima)
        {
            return new ClimaResponseDto
            {
                Cidade = clima.Cidade,
                Temperatura = clima.Temperatura,
                Condicao = clima.Condicao,
                Umidade = clima.Umidade,
                VelocidadoVento = clima.VelocidadoVento
            };
        }

        public static List<PrevisaoResponseDto> ToPrevisaoResponse(this List<Previsao> previsoes)
        {
            return previsoes.Select(p => new PrevisaoResponseDto
            {
                Cidade = p.Cidade,
                Data = p.Data,
                TemperaturaMin = p.TemperaturaMin,
                TemperaturaMax = p.TemperaturaMax,
                Condicao = p.Condicao,
                HumidadeMedia = p.HumidadeMedia
            }).ToList();
        }

        public static List<HistoricoResponseDto> ToHistoricoResponse(this List<Historico> historico)
        {
            return historico.Select(h => new HistoricoResponseDto
            {
                Cidade = h.Cidade,
                DataConsulta = h.DataConsulta
            }).ToList();
        }
    }
}
