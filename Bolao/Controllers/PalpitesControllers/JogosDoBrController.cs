using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class JogosDoBrController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public JogosDoBrController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] JogosDoBrEditarRequest oitavasRequest)
        {
            var retorno = palpiteServico.EditaJogosDoBr(oitavasRequest);
            return Ok(retorno);
        }
    }
}
