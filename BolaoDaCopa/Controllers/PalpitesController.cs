using BolaoDaCopa.Aplicacao.Boloes.Servicos;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Palpite.Requests;
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
    }
}
