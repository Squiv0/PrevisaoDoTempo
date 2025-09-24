namespace Dados.Entidades
{
    public class Historico
    {
        public int Id { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public DateTime DataConsulta { get; set; } = DateTime.UtcNow;
        public bool IsClima { get; set; } = false;
        public bool IsPrevisao { get; set; } = false;
    }
}
