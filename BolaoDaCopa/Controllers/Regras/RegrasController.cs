using BolaoDaCopa.Aplicacao.Regras.Servicos.Interfaces;
using BolaoDaCopa.Dto.Regras.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers.Regras
{
    [Authorize]
    [ApiController]
    [Route("api/regras")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class RegrasController : ControllerBase
    {
        private readonly IRegrasServico _regrasServico;

        public RegrasController(IRegrasServico regrasServico)
        {
            _regrasServico = regrasServico;
        }

        /// <summary>
        /// Listar Regras
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<RegraResponse>> ListarRegras()
        {
            IEnumerable<RegraResponse> response = _regrasServico.Listar();

            return Ok(response);
        }
    }
}
