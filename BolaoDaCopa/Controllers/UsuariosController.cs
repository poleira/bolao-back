using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.Autenticacao.Responses;
using BolaoDaCopa.Dto.Usuarios.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/usuario")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServico _usuariosServico;

        public UsuariosController(IUsuariosServico _usuariosServico)
        {
            this._usuariosServico = _usuariosServico;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("inserir")]
        public ActionResult<AutenticacaoResponse> Inserir([FromBody] UsuarioRequest request)
        {
            AutenticacaoResponse? response = _usuariosServico.Inserir(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("autenticar")]
        public async Task<ActionResult<AutenticacaoResponse>> Autenticar([FromBody] LoginRequest request)
        {
            AutenticacaoResponse response = await _usuariosServico.Autenticar(request);

            return Ok(response);
        }
    }
}
