
using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class GaController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public GaController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] GaEditarRequest gaRequest)
        {
            var retorno = palpiteServico.EditaGa(gaRequest);
            return Ok(retorno);
        }
    }
}
