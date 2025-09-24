using Servicos.DTOs;
using Servicos.Utils;

namespace Servicos.Interfaces
{
    public interface IHistoricoService
    {
        Task<List<HistoricoResponseDto>> ExibirHistoricoAsync();
        Task AdicionarHistoricoAsync(string cidade, TipoHistorico tipoHistorico);
        Task<bool> ValidarExistenciaHistorico(string cidade, TipoHistorico tipoHistorico);
    }
}
