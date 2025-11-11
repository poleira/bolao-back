using BolaoDaCopa.Aplicacao.Boloes.Servicos;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using BolaoDaCopa.Dto.Selecoes.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BolaoDaCopa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/palpites")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class PalpitesController : Controller
    {
        private readonly IPalpitesServico palpitesServico;
        public PalpitesController(IPalpitesServico palpitesServico)
        {
            this.palpitesServico = palpitesServico;
        }

        /// <summary>
        /// Cria PalpiteArtilheiro
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("artilheiros")]
        public async Task<ActionResult> CriarPalpiteArtilheiroAsync([FromBody] CriarPalpiteArtilheiroRequest request)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim != null && int.TryParse(idClaim.Value, out int idUsuario))
            {
                request.IdUsuario = idUsuario;
            }
            else
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }

            await palpitesServico.CriarPalpiteArtilheiro(request);
            return Ok();
        }

        /// <summary>
        /// Cria PalpiteFaseSelecao
        /// </summary>
        [HttpPost]
        [Route("fases-selecoes")]
        public async Task<IActionResult> CriarPalpiteFaseSelecao([FromBody] CriarPalpiteFaseSelecaoRequest[] request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            await palpitesServico.CriarPalpiteFaseSelecao(request, idUsuario);
            return Ok();
        }


        /// <summary>
        /// Cria PalpiteGrupoSelecao
        /// </summary>
        [HttpPost]
        [Route("grupos-selecoes")]
        public async Task<IActionResult> CriarPalpiteGrupoSelecao([FromBody] CriarPalpiteGrupoSelecaoRequest[] request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            await palpitesServico.CriarPalpiteGrupoSelecao(request, idUsuario);
            return Ok();
        }


        /// <summary>
        /// Cria PalpiteJogoGrupo
        /// </summary>
        [HttpPost]
        [Route("jogos-grupos")]
        public async Task<IActionResult> CriarPalpiteJogoGrupo([FromBody] CriarPalpiteJogoGrupoRequest[] request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            await palpitesServico.CriarPalpiteJogoGrupo(request, idUsuario);
            return Ok();
        }

        /// <summary>
        /// Recupera Palpite artilheiro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("artilheiros")]
        public async Task<ActionResult<PalpiteArtilheiroResponse>> RecuperarPalpiteArtilheiroAsync([FromQuery] HashBolaoRequest request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            var retorno = await palpitesServico.RecuperarPalpiteArtilheiroAsync(request.HashBolao, idUsuario);
            return Ok(retorno);
        }


        /// <summary>
        /// Recupera PalpiteFaseSelecao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("fases-selecoes")]
        public async Task<ActionResult<PalpiteFaseSelecaoResponse>> RecuperarPalpiteFaseSelecaoAsync([FromQuery] HashBolaoRequest request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            var retorno = await palpitesServico.RecuperarPalpiteFaseSelecaoAsync(request.HashBolao, idUsuario);
            return Ok(retorno);
        }


        /// <summary>
        /// Recupera PalpiteGrupoSelecao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("grupos-selecoes")]
        public async Task<ActionResult<IList<PalpiteGrupoSelecaoResponse>>> RecuperarPalpiteGrupoSelecaoAsync([FromQuery] HashBolaoRequest request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            var retorno = await palpitesServico.RecuperarPalpiteGrupoSelecaoAsync(request.HashBolao, idUsuario);
            return Ok(retorno);
        }


        /// <summary>
        /// Recupera PalpiteJogoGrupo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("jogos-grupos")]
        public async Task<ActionResult<PalpiteJogoGrupoResponse>> RecuperarPalpiteJogoGrupoAsync([FromQuery] HashBolaoRequest request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            var retorno = await palpitesServico.RecuperarPalpiteJogoGrupoAsync(request.HashBolao, idUsuario);
            return Ok(retorno);
        }

        /// <summary>
        /// Recupera Palpite terceiro lugares
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("terceiros-lugares")]
        public async Task<ActionResult<IList<GrupoSelecaoResponse>>> RecuperarPalpiteMelhoresTerceiroLugarAsync([FromQuery] HashBolaoRequest request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            var retorno = await palpitesServico.RecuperarPalpiteMelhoresTerceiroLugarAsync(request.HashBolao, idUsuario);
            return Ok(retorno);
        }

    }
}
