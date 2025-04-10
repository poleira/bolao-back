using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.JogosBr;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers.JogosBr
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class JogosBrGruposController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public JogosBrGruposController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] FaseDeGruposJogosBrRequest oitavasRequest)
        {
            var retorno = palpiteServico.EditaJogosBrGrupos(oitavasRequest);
            return Ok(retorno);
        }
    }
}
