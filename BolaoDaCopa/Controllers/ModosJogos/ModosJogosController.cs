using BolaoDaCopa.Aplicacao.ModosJogos.Servicos.Interfaces;
using BolaoDaCopa.Dto.Regras.Responses;
using BolaoDaCopa.Dto.ModosJogos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BolaoDaCopa.Dto.Boloes.Requests;

namespace BolaoDaCopa.Controllers.ModosJogos
{
    [Authorize]
    [ApiController]
    [Route("api/modos-jogos")]
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

        /// <summary>
        /// Retorna o ModoJogo associado a um bolão através do hash do bolão (hash passado no body)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("boloes")]
        public ActionResult<ModoJogoResponse> ObterModoPorHashBolao([FromQuery] HashBolaoRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.HashBolao)) return BadRequest();

            var modo = _modosJogosServicos.ObterModoPorHashBolao(request.HashBolao);
            if (modo == null) return NotFound();
            return Ok(modo);
        }
    }
}
