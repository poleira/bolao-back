using BolaoDaCopa.Aplicacao.Boloes.Servicos;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
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
        public ActionResult CriarPalpiteArtilheiro([FromBody] CriarPalpiteArtilheiroRequest request)
        {
            palpitesServico.CriarPalpiteArtilheiro(request);
            return Ok();
        }

        /// <summary>
        /// Cria PalpiteFaseSelecao
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("fases-selecoes")]
        public async Task<ActionResult> CriarPalpiteFaseSelecao([FromBody] CriarPalpiteFaseSelecaoRequest[] request)
        {
            await palpitesServico.CriarPalpiteFaseSelecao(request);
            return Ok();
        }

        /// <summary>
        /// Cria PalpiteGrupoSelecao
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("grupos-selecoes")]
        public async Task<ActionResult> CriarPalpiteGrupoSelecao([FromBody] CriarPalpiteGrupoSelecaoRequest[] request)
        {
            await palpitesServico.CriarPalpiteGrupoSelecao(request);
            return Ok();
        }

        /// <summary>
        /// Cria PalpiteJogoGrupo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("jogos-grupos")]
        public async Task<ActionResult> CriarPalpiteJogoGrupo([FromBody] CriarPalpiteJogoGrupoRequest[] request)
        {
            await palpitesServico.CriarPalpiteJogoGrupo(request);
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
            string usuarioHash = "8kVzFTYZiIRlvoRa7kKRt4bTEvn2";
            var retorno = await palpitesServico.RecuperarPalpiteArtilheiroAsync(request.HashBolao, usuarioHash);
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
            string usuarioHash = "8kVzFTYZiIRlvoRa7kKRt4bTEvn2";
            var retorno = await palpitesServico.RecuperarPalpiteFaseSelecaoAsync(request.HashBolao, usuarioHash);
            return Ok(retorno);
        }

        /// <summary>
        /// Recupera PalpiteGrupoSelecao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("grupos-selecoes")]
        public async Task<ActionResult<PalpiteGrupoSelecaoResponse>> RecuperarPalpiteGrupoSelecaoAsync([FromQuery] HashBolaoRequest request)
        {
            string usuarioHash = "8kVzFTYZiIRlvoRa7kKRt4bTEvn2";
            var retorno = await palpitesServico.RecuperarPalpiteGrupoSelecaoAsync(request.HashBolao, usuarioHash);
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
            string usuarioHash = "8kVzFTYZiIRlvoRa7kKRt4bTEvn2";
            var retorno = await palpitesServico.RecuperarPalpiteJogoGrupoAsync(request.HashBolao, usuarioHash);
            return Ok(retorno);
        }
    }
}
