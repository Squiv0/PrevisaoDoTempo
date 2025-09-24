using Servicos.DTOs;

namespace Servicos.Interfaces
{
    public interface IPrevisaoService
    {
        Task<List<PrevisaoResponseDto>> ObterPrevisao5DiasAsync(string cidade);
    }
}
