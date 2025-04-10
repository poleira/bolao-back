using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class FinaisController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public FinaisController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] FinaisEditarRequest oitavasRequest)
        {
            var retorno = palpiteServico.EditaFinais(oitavasRequest);
            return Ok(retorno);
        }
    }
}
