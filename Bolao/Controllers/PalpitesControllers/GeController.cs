using BolaoTeste.Aplicacao.Cadastros.Servicos.Interfaces;
using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class GeController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public GeController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] GeEditarRequest gaRequest)
        {
            var retorno = palpiteServico.EditaGe(gaRequest);
            return Ok(retorno);
        }
    }
}
