using Dados.Entidades;
using Dados.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Servicos.Configuracoes;
using Servicos.DTOs;
using Servicos.Interfaces;
using Servicos.Servicos;
using Servicos.Utils;

namespace Testes
{
    public class ClimaServiceTestes
    {
        private readonly IOptions<WeatherApiConfiguracao> _config =
        Options.Create(new WeatherApiConfiguracao());

        [Fact]
        public async Task Teste1_RetornaDoBancoQuandoTemHistorico()
        {
            //Arrange
            var mockRepo = new Mock<IClimaRepository>();
            var mockHistorico = new Mock<IHistoricoService>();
            var clima = new Clima
            {
                Cidade = "Sao Paulo",
                Temperatura = 25
            };
            mockHistorico.Setup(x => x.ValidarExistenciaHistorico("Sao Paulo", TipoHistorico.CLIMA)).ReturnsAsync(true);
            mockRepo.Setup(x => x.ObterAsync("Sao Paulo")).ReturnsAsync(clima);
            var service = new ClimaService(_config, mockRepo.Object, mockHistorico.Object);

            //Act
            var resultado = await service.ObterClimaAsync("Sao Paulo");

            //Assert
            Assert.Equal("Sao Paulo", resultado.Cidade);
        }

        [Fact]
        public async Task Teste2_TentaApiQuandoNaoTemHistorico()
        {
            //Arrange
            var mockRepo = new Mock<IClimaRepository>();
            var mockHistorico = new Mock<IHistoricoService>();

            mockHistorico.Setup(x => x.ValidarExistenciaHistorico("Suzano", TipoHistorico.CLIMA)).ReturnsAsync(false);
            var service = new ClimaService(_config, mockRepo.Object, mockHistorico.Object);

            //Act
            var excecao = await Record.ExceptionAsync(async () => await service.ObterClimaAsync("Suzano"));

            //Assert
            Assert.NotNull(excecao);
        }
    }
}
