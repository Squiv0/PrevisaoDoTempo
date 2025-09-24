namespace Dados.Entidades
{
    public class Previsao
    {
        public int Id { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public double TemperaturaMin { get; set; }
        public double TemperaturaMax { get; set; }
        public string Condicao { get; set; } = string.Empty;
        public int HumidadeMedia { get; set; }
    }
}
