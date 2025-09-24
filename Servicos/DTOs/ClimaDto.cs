namespace Servicos.DTOs
{
    public record class ClimaDto
    {
        public LocationDto? Location { get; init; }
        public CurrentDto? Current { get; init; }
    }

    public record class LocationDto
    {
        public string? Name { get; init; }
    }

    public record class CurrentDto
    {
        public double Temp_C { get; init; }
        public ConditionDto? Condition { get; init; }
        public double Wind_Kph { get; init; }
        public int Humidity { get; init; }
    }

    public record class ConditionDto
    {
        public string? Text { get; init; }
    }
}
