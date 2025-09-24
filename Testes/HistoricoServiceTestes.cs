using Dados.Entidades;
using Dados.Interfaces;
using Moq;
using Servicos.Interfaces;
using Servicos.Servicos;
using Servicos.Utils;
using Xunit;


namespace Testes
{
    public class HistoricoServiceTestes
    {
        [Fact]
        public async Task Teste1_RetornaTrueQuandoTemHistorico()
        {
            //Arrange
            var mockRepo = new Mock<IHistoricoRepository>();
            var historicos = new List<Historico>
            {
                new Historico { Cidade = "Sao Paulo", IsClima = true }
            };
            mockRepo.Setup(x => x.ListarAsync()).ReturnsAsync(historicos);
            var service = new HistoricoService(mockRepo.Object);

            //Act
            var resultado = await service.ValidarExistenciaHistorico("Sao Paulo", TipoHistorico.CLIMA);

            //Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task Teste2_RetornaFalseQuandoNaoTemHistorico()
        {
            //Arrange
            var mockRepo = new Mock<IHistoricoRepository>();
            mockRepo.Setup(x => x.ListarAsync()).ReturnsAsync(new List<Historico>());
            var service = new HistoricoService(mockRepo.Object);

            //Act
            var resultado = await service.ValidarExistenciaHistorico("Suzano", TipoHistorico.CLIMA);

            //Assert
            Assert.False(resultado);
        }
    }
}
