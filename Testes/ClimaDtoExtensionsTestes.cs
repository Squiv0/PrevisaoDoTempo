using Dados.Entidades;
using Servicos.DTOs;
using Servicos.Extensions;

namespace Testes
{
    public class ClimaDtoExtensionsTestes
    {
        [Fact]
        public void Teste1_ConverteDtoParaEntidade()
        {
            //Arrange
            var dto = new ClimaDto
            {
                Location = new LocationDto { Name = "Sao Paulo" },
                Current = new CurrentDto { Temp_C = 25, Humidity = 80 }
            };

            //Act
            var resultado = dto.ToEntity();

            //Assert
            Assert.Equal("Sao Paulo", resultado.Cidade);
            Assert.Equal(25, resultado.Temperatura);
        }

        [Fact]
        public void Teste2_ConverteEntidadeParaDto()
        {
            //Arrange
            var entidade = new Clima { Cidade = "Suzano", Temperatura = 30 };

            //Act
            var resultado = entidade.ToDto();

            //Assert
            Assert.Equal("Suzano", resultado.Location.Name);
            Assert.Equal(30, resultado.Current.Temp_C);
        }
    }
}
