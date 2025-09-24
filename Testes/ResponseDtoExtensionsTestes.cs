using Servicos.Extensions;
using Dados.Entidades;
namespace Testes
{
    public class ResponseDtoExtensionsTestes
    {
        [Fact]
        public void Teste1_ConverteClimaParaResponse()
        {
            //Arrange
            var clima = new Clima 
            { 
                Cidade = "Sao Paulo", 
                Temperatura = 25 
            };

            //Act
            var resultado = clima.ToClimaResponse();

            //Assert
            Assert.Equal("Sao Paulo", resultado.Cidade);
            Assert.Equal(25, resultado.Temperatura);
        }
    }
}
