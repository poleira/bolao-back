using System.Security.Claims;
using BolaoDaCopa.Aplicacao.JogosGrupo.Servicos.Interfaces;
using BolaoDaCopa.Dto.JogosGrupo.Requests;
using BolaoDaCopa.Dto.JogosGrupo.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/jogos-grupo")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class JogosGrupoController : Controller
    {
        private readonly IJogosGrupoServico jogosGrupoServico;

        public JogosGrupoController(IJogosGrupoServico jogosGrupoServico)
        {
            this.jogosGrupoServico = jogosGrupoServico;
        }

        /// <summary>
        /// Recupera todos os jogos da fase de grupos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IList<JogoGrupoResponse>> ListarJogosGrupo([FromQuery] JogoGrupoRequest request)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            var retorno = jogosGrupoServico.ListarJogosGrupo(request);
            return Ok(retorno);
        }

        /// <summary>
        /// Recupera todos os jogos da fase de grupos do Brasil
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("brasil")]
        public ActionResult<IList<JogoGrupoResponse>> ListarJogosBrasil()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario))
                return Unauthorized("ID do usuário inválido ou ausente.");

            var retorno = jogosGrupoServico.ListarJogosBrasil();
            return Ok(retorno);
        }
    }
}
