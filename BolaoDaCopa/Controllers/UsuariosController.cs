using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.Autenticacao.Responses;
using BolaoDaCopa.Dto.Usuarios.Requests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BolaoDaCopa.Controllers
{
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

        [HttpPost]
        [Route("inserir")]
        public ActionResult<AutenticacaoResponse> Inserir([FromBody] UsuarioRequest request)
        {
            AutenticacaoResponse? response = _usuariosServico.Inserir(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("autenticar")]
        public async Task<ActionResult<AutenticacaoResponse>> Autenticar([FromBody] LoginRequest request)
        {
            AutenticacaoResponse response = await _usuariosServico.Autenticar(request);

            return Ok(response);
        }
    }
}
