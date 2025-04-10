using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class SemisController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public SemisController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] SemisEditarRequest semisRequest)
        {
            var retorno = palpiteServico.EditaSemis(semisRequest);
            return Ok(retorno);
        }
    }
}
