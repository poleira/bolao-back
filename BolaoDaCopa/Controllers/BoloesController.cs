using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.BoloesRegras.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BolaoDaCopa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/boloes")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class BoloesController : Controller
    {
        private readonly IBoloesServico boloesServico;
        public BoloesController(IBoloesServico boloesServico)
        {
            this.boloesServico = boloesServico;
        }

        /// <summary>
        /// Cria Bolao
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BolaoResponse> CriarBolao([FromBody] CriarBolaoRequest request)
        {
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            BolaoResponse? response = boloesServico.CriarBolao(request);

            return Ok(response);
        }

        /// <summary>
        /// Edita Bolao
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ActionResult EditarBolao([FromBody] EditarBolaoRequest request)
        {
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            boloesServico.EditarBolao(request);
            return Ok();
        }

        /// <summary>
        /// Recupera info Bolao
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BolaoResponse> Recuperar([FromQuery] HashBolaoRequest request)
        {
            var retorno = boloesServico.Recuperar(request.HashBolao);
            return Ok(retorno);
        }

        /// <summary>
        /// Associar Usuario a um Bolao
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("boloes-usuarios")]
        public ActionResult AssociarUsuarioBolao([FromBody] AssociarUsuarioRequest request)
        {
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            boloesServico.AssociarUsuarioBolao(request);
            return Ok();
        }

        /// <summary>
        /// Desassociar Usuario a um Bolao
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("usuarios")]
        public ActionResult DesassociarUsuarioBolao([FromBody] AssociarUsuarioRequest request)
        {
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            boloesServico.DesassociarUsuarioBolao(request);
            return Ok();
        }

        /// <summary>
        /// Inserir regras a um Bolao
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("regras-bolao")]
        public ActionResult InserirRegrasBolao([FromBody] InserirRegraBolaoRequest[] request)
        {
            boloesServico.InserirRegrasBolao(request, null);
            return Ok();
        }

        /// <summary>
        /// Listar Regras gerais
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("regras")]
        public ActionResult<IList<RegraResponse>> ListarRegras()
        {
            IList<RegraResponse>? response = boloesServico.ListarRegras();

            return Ok(response);
        }

        /// <summary>
        /// Listar Regras de um Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("regras-boloes")]
        public ActionResult<IList<BolaoRegraResponse>> ListarRegrasBolao([FromQuery] HashBolaoRequest request)
        {
            var retorno = boloesServico.ListarRegrasBolao(request.HashBolao);
            return Ok(retorno);
        }

        /// <summary>
        /// Listar Regras de um Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("premios-boloes")]
        public ActionResult<IList<PremioResponse>> ListarPremiosBolao([FromQuery] HashBolaoRequest request)
        {
            var retorno = boloesServico.ListarPremiosBolao(request.HashBolao);
            return Ok(retorno);
        }

        /// <summary>
        /// Inserir premios a um Bolao
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("premios-bolao")]
        public ActionResult InserirPremiosBolao([FromBody] InserirPremioBolaoRequest[] request)
        {
            boloesServico.InserirPremiosBolao(request, null);
            return Ok();
        }

    }
}

