using BolaoDaCopa.Aplicacao.ModoJogo.Servicos.Interfaces;
using BolaoDaCopa.Dto.Regras.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers.ModoJogo
{
    [Authorize]
    [ApiController]
    [Route("api/modojogo")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ModoJogoController : ControllerBase
    {
        private readonly IModoJogoServico _modoJogoServico;

        public ModoJogoController(IModoJogoServico modoJogoServico)
        {
            _modoJogoServico = modoJogoServico;
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
            var retorno = _modoJogoServico.ListarRegrasModoJogo(idModoJogo);
            return Ok(retorno);
        }
    }
}
