using Moq;
using Xunit;
using Microsoft.Extensions.Options;
using Servicos.Configuracoes;
using Servicos.Servicos;
using Servicos.Interfaces;
using Dados.Interfaces;
using Dados.Entidades;
using Servicos.Utils;

namespace Testes
{
    public class PrevisaoServiceTestes
    {
        private readonly IOptions<WeatherApiConfiguracao> _config =
            Options.Create(new WeatherApiConfiguracao());

        [Fact]
        public async Task Teste1_RetornaPrevisaoDoBancoQuandoTemHistorico()
        {
            //Arrange
            var mockRepo = new Mock<IPrevisaoRepository>();
            var mockHistorico = new Mock<IHistoricoService>();
            mockHistorico.Setup(x => x.ValidarExistenciaHistorico("São Paulo", TipoHistorico.PREVISAO)).ReturnsAsync(true);
            var previsoes = new List<Previsao>
            {
                new Previsao { Cidade = "São Paulo", TemperaturaMax = 28 }
            };
            mockRepo.Setup(x => x.ObterAsync("São Paulo")).ReturnsAsync(previsoes);
            var service = new PrevisaoService(_config, mockRepo.Object, mockHistorico.Object);

            //Act
            var resultado = await service.ObterPrevisao5DiasAsync("São Paulo");

            //Assert
            Assert.Single(resultado);
            Assert.Equal("São Paulo", resultado[0].Cidade);
        }

        [Fact]
        public async Task Teste2_TentaApiQuandoNaoTemHistorico()
        {
            //Arrange
            var mockRepo = new Mock<IPrevisaoRepository>();
            var mockHistorico = new Mock<IHistoricoService>();
            mockHistorico.Setup(x => x.ValidarExistenciaHistorico("Rio", TipoHistorico.PREVISAO)).ReturnsAsync(false);
            var service = new PrevisaoService(_config, mockRepo.Object, mockHistorico.Object);

            //Act
            var excecao = await Record.ExceptionAsync(async () => await service.ObterPrevisao5DiasAsync("Rio"));

            //Assert
            Assert.NotNull(excecao);
        }
    }
}
