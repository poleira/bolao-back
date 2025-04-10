using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{

        [ApiController]
        [Route("api/[controller]")]
        [EnableCors("MyCorsImplementationPolicy")]
        public class OitavasController : Controller
        {
            private readonly IPalpiteServico palpiteServico;
            public OitavasController(IPalpiteServico gaServico)
            {
                this.palpiteServico = gaServico;
            }

            [HttpPost]
            public ActionResult Listar([FromBody] ListarOitavasRequest oitavasRequest)
            {
                var retorno = palpiteServico.ListarOitavas(oitavasRequest);
                if (retorno == null)
                    return BadRequest("Erro ao tentar salvar, tente novamente mais tarde");
                return Ok(retorno);
            }
        }
    }

