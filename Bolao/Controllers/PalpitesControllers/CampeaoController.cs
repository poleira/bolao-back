using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers.PalpitesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class CampeaoController: Controller
    {
        private readonly IPalpiteServico palpiteServico;
        public CampeaoController(IPalpiteServico gaServico)
        {
            this.palpiteServico = gaServico;
        }

        [HttpPut]
        public ActionResult Editar([FromBody] CampeaoEditarRequest oitavasRequest)
        {
            var retorno = palpiteServico.EditaCampeao(oitavasRequest);
            return Ok(retorno);
        }
    }
}
