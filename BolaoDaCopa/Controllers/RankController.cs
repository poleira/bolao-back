using BolaoTeste.Dto.Rank;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;

namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class RankController : Controller
    {
        private readonly IRankServico rankServico;
        public RankController(IRankServico rankServico)
        {
            this.rankServico = rankServico;
        }

        [HttpGet]
        public ActionResult<IList<RankResponse>> ListarRank()
        {
            var retorno = rankServico.ListarRank();
            return Ok(retorno);
        }
    }
}
