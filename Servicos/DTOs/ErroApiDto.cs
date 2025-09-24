using Newtonsoft.Json;

namespace Servicos.DTOs
{
    public class ErroApiDto
    {
        [JsonProperty("code")]
        public int Codigo { get; set; }

        [JsonProperty("message")]
        public string Mensagem { get; set; } = string.Empty;
    }

    public class ErroApiRespostaDto
    {
        [JsonProperty("error")]
        public ErroApiDto Erro { get; set; } = new ErroApiDto();
    }
}
