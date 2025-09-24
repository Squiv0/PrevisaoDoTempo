namespace Servicos.DTOs
{
    public record class PrevisaoDto
    {
        public LocationDto? Location { get; init; }
        public ForecastDto? Forecast { get; init; }
    }

    public record class ForecastDto
    {
        public List<ForecastDayDto>? Forecastday { get; init; }
    }

    public record class ForecastDayDto
    {
        public string? Date { get; init; }
        public DayDto? Day { get; init; }
    }

    public record class DayDto
    {
        public double Maxtemp_C { get; init; }
        public double Mintemp_C { get; init; }
        public int Avghumidity { get; init; }
        public ConditionDto? Condition { get; init; }
    }

}
