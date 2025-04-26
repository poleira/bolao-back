using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult CriarBolao([FromBody] CriarBolaoRequest request)
        {
            boloesServico.CriarBolao(request);
            return Ok();
        }

        /// <summary>
        /// Recupera info Bolao
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{hash}")]
        public ActionResult<Bolao> Recuperar([FromRoute] string hash)
        {
            var retorno = boloesServico.Recuperar(hash);
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
        public ActionResult InserirRegrasBolao([FromBody] InserirRegrasBolaoRequest[] request)
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
        public ActionResult<IList<Regra>> ListarRegras()
        {
            var retorno = boloesServico.ListarRegras();
            return Ok(retorno);
        }

        /// <summary>
        /// Listar Regras de um Bolao
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("regras-boloes/{hash}")]
        public ActionResult<IList<BolaoRegraResponse>> ListarRegrasBolao([FromRoute] string hash)
        {
            var retorno = boloesServico.ListarRegrasBolao(hash);
            return Ok(retorno);
        }
    }
}

