using Dados.Entidades;
using Servicos.DTOs;

namespace Servicos.Extensions
{
    public static class ClimaDtoExtensions
    {
        public static Clima ToEntity(this ClimaDto dto)
        {
            return new Clima
            {
                Cidade = dto.Location?.Name ?? string.Empty,
                Temperatura = dto.Current?.Temp_C ?? 0,
                Umidade = dto.Current?.Humidity ?? 0,
                Condicao = dto.Current?.Condition?.Text ?? string.Empty,
                VelocidadoVento = dto.Current?.Wind_Kph ?? 0
            };
        }

        public static ClimaDto ToDto(this Clima entity)
        {
            return new ClimaDto
            {
                Location = new LocationDto
                {
                    Name = entity.Cidade
                },
                Current = new CurrentDto
                {
                    Temp_C = entity.Temperatura,
                    Humidity = entity.Umidade,
                    Wind_Kph = entity.VelocidadoVento,
                    Condition = new ConditionDto
                    {
                        Text = entity.Condicao
                    }
                }
            };
        }
    }
}
