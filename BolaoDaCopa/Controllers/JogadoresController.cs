using BolaoDaCopa.Aplicacao.Jogadores.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Jogadores.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using BolaoDaCopa.Models;
using BolaoTeste.Dto.Rank;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/jogadores")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class JogadoresController : Controller
    {
        private readonly IJogadoresServico jogadoresServico;
        public JogadoresController(IJogadoresServico jogadoresServico)
        {
            this.jogadoresServico = jogadoresServico;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Jogador>> ListarJogadores([FromQuery] JogadoresListarRequest request)
        {
            var retorno = jogadoresServico.ListarJogadores(request);
            return Ok(retorno);
        }
    }
}
