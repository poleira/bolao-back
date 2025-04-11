using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Cadastros;
using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Rank;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class PainelController : Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public PainelController(IPalpiteServico palpite)
        {
            palpiteServico = palpite;
        }

        [HttpPost]
        public ActionResult<ListarPalpiteResponse> ListarPalpites([FromBody] ListarPalpiteRequest request)
        {
            //var retorno = palpiteServico.ListarPalpites(request);
            return Ok(null);
        }
    }
}
