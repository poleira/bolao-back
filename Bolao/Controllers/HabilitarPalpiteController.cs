using BolaoTeste.Aplicacao.Cadastros.Servicos.Interfaces;
using BolaoTeste.Aplicacao.HabilitarPalpites.Servicos.Interfaces;
using BolaoTeste.Aplicacao.Rank.Servicos;
using BolaoTeste.Dto.HabilitarPalpites;
using BolaoTeste.Dto.Rank;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoTeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class HabilitarPalpiteController : Controller
    {
        private readonly IHabilitarPalpiteServico habilitarPalpiteServico;
        public HabilitarPalpiteController(IHabilitarPalpiteServico habilitarPalpiteServico)
        {
            this.habilitarPalpiteServico = habilitarPalpiteServico;
        }

        [HttpGet]
        public ActionResult<HabilitarPalpiteResponse> Recuperar()
        {
            var retorno = habilitarPalpiteServico.Recuperar();
            return Ok(retorno);
        }
    }
}
