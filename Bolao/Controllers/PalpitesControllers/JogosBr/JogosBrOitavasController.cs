using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.JogosBr;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers.JogosBr
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class JogosBrOitavasController: Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public JogosBrOitavasController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] MataMataJogosBrRequest oitavasRequest)
        {
            var retorno = palpiteServico.EditaJogosBrOitavas(oitavasRequest);
            return Ok(retorno);
        }
    }
}
