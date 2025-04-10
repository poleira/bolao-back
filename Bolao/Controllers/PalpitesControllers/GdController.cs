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
    public class GdController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public GdController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] GdEditarRequest gaRequest)
        {
            var retorno = palpiteServico.EditaGd(gaRequest);
            return Ok(retorno);
        }
    }
}
