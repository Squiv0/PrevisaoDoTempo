using Servicos.DTOs;

namespace Servicos.Interfaces
{
    public interface IClimaService
    {
        Task<ClimaResponseDto> ObterClimaAsync(string cidade);
    }
}
