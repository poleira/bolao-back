using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.JogosBr;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers.JogosBr
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class JogosBrSemisController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public JogosBrSemisController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] MataMataJogosBrRequest oitavasRequest)
        {
            var retorno = palpiteServico.EditaJogosBrSemis(oitavasRequest);
            return Ok(retorno);
        }
    }
}
