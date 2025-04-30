using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Selecoes.Requests;
using BolaoDaCopa.Dto.Selecoes.Responses;
using BolaoDaCopa.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class SelecoesController : Controller
    {
        private readonly ISelecoesServico habilitarPalpiteServico;
        public SelecoesController(ISelecoesServico habilitarPalpiteServico)
        {
            this.habilitarPalpiteServico = habilitarPalpiteServico;
        }

        /// <summary>
        /// Recupera info Grupos Selecoes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IList<GrupoSelecaoResponse>> ListarSelecoes([FromQuery] GrupoSelecaoRequest request)
        {
            var retorno = habilitarPalpiteServico.ListarSelecoes(request);
            return Ok(retorno);
        }
    }
}
