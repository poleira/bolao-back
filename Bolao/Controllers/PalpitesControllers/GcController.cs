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
    public class GcController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public GcController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] GcEditarRequest gaRequest)
        {
            var retorno = palpiteServico.EditaGc(gaRequest);
            return Ok(retorno);
        }
    }
}
