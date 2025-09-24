namespace Dados.Entidades
{
    public class Clima
    {
        public int Id { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public double Temperatura { get; set; }
        public int Umidade { get; set; }
        public string Condicao { get; set; } = string.Empty;
        public double VelocidadoVento { get; set; }
    }
}
