using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ISelecaoAtualizacaoServico selecaoAtualizacaoServico;
        private readonly IFaseSelecaoAtualizacaoServico faseSelecaoAtualizacaoServico;
        private readonly ILogger<JobsController> logger;

        public JobsController(
            ISelecaoAtualizacaoServico selecaoAtualizacaoServico,
            IFaseSelecaoAtualizacaoServico faseSelecaoAtualizacaoServico,
            ILogger<JobsController> logger)
        {
            this.selecaoAtualizacaoServico = selecaoAtualizacaoServico;
            this.faseSelecaoAtualizacaoServico = faseSelecaoAtualizacaoServico;
            this.logger = logger;
        }

        [HttpPost("executar-atualizacao-selecoes")]
        public async Task<IActionResult> ExecutarAtualizacaoSelecoes()
        {
            try
            {
                logger.LogInformation("Executando AtualizarStandings manualmente via API...");
                await selecaoAtualizacaoServico.AtualizarStandings();
                return Ok(new { message = "Atualização de standings executada com sucesso." });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao executar atualização de standings.");
                return StatusCode(500, new { error = ex.Message, innerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("executar-atualizacao-fases")]
        public async Task<IActionResult> ExecutarAtualizacaoFases()
        {
            try
            {
                logger.LogInformation("Executando AtualizarFasesEliminatorias manualmente via API...");
                await faseSelecaoAtualizacaoServico.AtualizarFasesEliminatorias();
                return Ok(new { message = "Atualização de fases eliminatórias executada com sucesso." });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao executar atualização de fases eliminatórias.");
                return StatusCode(500, new { error = ex.Message, innerException = ex.InnerException?.Message });
            }
        }
    }
}
