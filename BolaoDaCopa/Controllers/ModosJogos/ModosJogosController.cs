using BolaoDaCopa.Aplicacao.ModosJogos.Servicos.Interfaces;
using BolaoDaCopa.Dto.Regras.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers.ModosJogos
{
    [Authorize]
    [ApiController]
    [Route("api/modosjogos")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ModosJogosController : ControllerBase
    {
        private readonly IModosJogosServicos _modosJogosServicos;

        public ModosJogosController(IModosJogosServicos modosJogosServicos)
        {
            _modosJogosServicos = modosJogosServicos;
        }

        /// <summary>
        /// Lista regras de um ModoJogo pelo id
        /// </summary>
        /// <param name="idModoJogo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idModoJogo}/regras")]
        public ActionResult<IEnumerable<RegraResponse>> ListarRegrasPorModo(int idModoJogo)
        {
            var retorno = _modosJogosServicos.ListarRegrasModoJogo(idModoJogo);
            return Ok(retorno);
        }
    }
}
