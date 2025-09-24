using Dados.Entidades;
using Servicos.DTOs;

namespace Servicos.Extensions
{
    public static class PrevisaoDtoExtensions
    {
        public static List<Previsao> ToEntityList(this PrevisaoDto dto)
        {
            if (dto.Forecast?.Forecastday == null || dto.Location == null)
                return new List<Previsao>();

            return dto.Forecast.Forecastday.Select(forecastDay => new Previsao
            {
                Cidade = dto.Location.Name ?? string.Empty,
                Data = DateTime.TryParse(forecastDay.Date, out var date) ? date : DateTime.MinValue,
                TemperaturaMin = forecastDay.Day?.Mintemp_C ?? 0,
                TemperaturaMax = forecastDay.Day?.Maxtemp_C ?? 0,
                HumidadeMedia = forecastDay.Day?.Avghumidity ?? 0,
                Condicao = forecastDay.Day?.Condition?.Text ?? string.Empty
            }).ToList();
        }

        public static PrevisaoDto ToDto(this List<Previsao> previsoes, string cidade)
        {
            if (previsoes == null || previsoes.Count == 0)
                return new PrevisaoDto
                {
                    Location = new LocationDto { Name = cidade },
                    Forecast = new ForecastDto { Forecastday = new List<ForecastDayDto>() }
                };

            var forecastDays = previsoes.Select(e => new ForecastDayDto
            {
                Date = e.Data.ToString("yyyy-MM-dd"),
                Day = new DayDto
                {
                    Maxtemp_C = e.TemperaturaMax,
                    Mintemp_C = e.TemperaturaMin,
                    Avghumidity = e.HumidadeMedia,
                    Condition = new ConditionDto { Text = e.Condicao }
                }
            }).ToList();

            return new PrevisaoDto
            {
                Location = new LocationDto { Name = cidade },
                Forecast = new ForecastDto { Forecastday = forecastDays }
            };
        }
    }
}
