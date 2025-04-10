using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class QuartasController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public QuartasController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] QuartasEditarRequest oitavasRequest)
        {
            var retorno = palpiteServico.EditaQuartas(oitavasRequest);
            if (retorno == null)
                return BadRequest("Erro ao tentar salvar, tente novamente mais tarde");
            return Ok(retorno);
        }
    }
}
