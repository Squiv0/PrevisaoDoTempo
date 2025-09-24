using Microsoft.AspNetCore.Mvc;
using Servicos.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/clima")]
    public class ClimaController : Controller
    {
        private readonly IClimaService _climaService;
        private readonly IPrevisaoService _previsaoService;
        private readonly IHistoricoService _historicoService;

        public ClimaController(IClimaService climaService, IPrevisaoService previsaoService, IHistoricoService historicoService)
        {
            _climaService = climaService;
            _previsaoService = previsaoService;
            _historicoService = historicoService;
        }

        [HttpGet("atual/{cidade}")]
        public async Task<IActionResult> ObterClimaAtual(string cidade)
        {
            try
            {
                var clima = await _climaService.ObterClimaAsync(cidade);

                return Ok(clima);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("previsao5dias/{cidade}")]
        public async Task<IActionResult> ObterPrevisao5Dias(string cidade)
        {
            try
            {
                var previsao = await _previsaoService.ObterPrevisao5DiasAsync(cidade);

                return Ok(previsao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("historico")]
        public async Task<IActionResult> ObterHistorico()
        {
            try
            {
                var historico = await _historicoService.ExibirHistoricoAsync();

                return Ok(historico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
