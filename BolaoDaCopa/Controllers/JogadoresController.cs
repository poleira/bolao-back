using BolaoDaCopa.Aplicacao.Jogadores.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Jogadores.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using BolaoTeste.Dto.Rank;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class JogadoresController : Controller
    {
        private readonly IJogadoresServico jogadoresServico;
        public JogadoresController(IJogadoresServico jogadoresServico)
        {
            this.jogadoresServico = jogadoresServico;
        }

        [HttpGet]
        public ActionResult<PalpiteFaseSelecaoResponse> ListarPalpites([FromQuery] JogadoresListarRequest request)
        {
            var retorno = jogadoresServico.ListarJogadores(request);
            return Ok(retorno);
        }
    }
}
