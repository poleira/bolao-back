using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoTeste.Dto.Rank;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BolaoDaCopa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/rank")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class RankController : Controller
    {
        private readonly IRankServico rankServico;
        public RankController(IRankServico rankServico)
        {
            this.rankServico = rankServico;
        }

        [HttpGet]
        public async Task<ActionResult<IList<RankResponse>>> ListarRank([FromQuery] HashBolaoRequest request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            if (request == null || string.IsNullOrWhiteSpace(request.HashBolao))
            {
                return BadRequest("HashBolao é obrigatório.");
            }

            var retorno = await rankServico.ListarRankAsync(request.HashBolao);
            return Ok(retorno);
        }
    }
}
