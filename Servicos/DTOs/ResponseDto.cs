namespace Servicos.DTOs
{
    public record class ClimaResponseDto
    {
        public string? Cidade { get; init; }
        public double Temperatura { get; init; }
        public int Umidade { get; init; }
        public string? Condicao { get; init; }
        public double VelocidadoVento { get; init; }
    }

    public record class PrevisaoResponseDto
    {
        public string? Cidade { get; init; }
        public DateTime Data { get; init; }
        public double TemperaturaMin { get; init; }
        public double TemperaturaMax { get; init; }
        public string? Condicao { get; init; }
        public int HumidadeMedia { get; init; }
    }

    public record class HistoricoResponseDto
    {
        public string? Cidade { get; init; }
        public DateTime DataConsulta { get; init; }
    }
}
