using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
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
            BolaoResponse? response = boloesServico.CriarBolao(request);

            return Ok(response);
        }

        ///// <summary>
        ///// Recupera info Bolao
        ///// </summary>
        ///// <param name="hash"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("{hash}")]
        //public ActionResult<Bolao> Recuperar([FromRoute] string hash)
        //{
        //    var retorno = boloesServico.Recuperar(hash);
        //    return Ok(retorno);
        //}

        /// <summary>
        /// Recupera boloes que um usuario esta associado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuarios")]
        public ActionResult<IList<BolaoResponse>> RecuperarBoloesUsuario([FromQuery] HashBolaoRequest request)
        {
            var retorno = boloesServico.RecuperarBoloesPorUsuario(request.HashBolao);
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
            boloesServico.InserirRegrasBolao(request);
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
            boloesServico.InserirPremiosBolao(request);
            return Ok();
        }

    }
}

