using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.Autenticacao.Responses;
using BolaoDaCopa.Dto.Usuarios;
using BolaoDaCopa.Dto.Usuarios.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet]
        [Route("me")]
        public ActionResult<UsuarioResponse> ObterUsuarioLogado()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null || !int.TryParse(idClaim.Value, out int idUsuario))
            {
                return Unauthorized("ID do usuário inválido ou ausente.");
            }

            UsuarioResponse response = _usuariosServico.ObterUsuarioLogado(idUsuario);

            return Ok(response);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("verificar-existente")]
        public ActionResult VerificarUsuarioExistente([FromBody] VerificarUsuarioExistenteRequest request)
        {
            _usuariosServico.VerificarUsuarioExistente(request);

            return Ok(new { mensagem = "Usuário disponível para cadastro." });
        }

        [HttpPut]
        [Route("alterar-nome")]
        public ActionResult AlterarNome([FromBody] AlterarNomeUsuarioRequest request)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null || !int.TryParse(idClaim.Value, out int idUsuario))
            {
                return Unauthorized("ID do usuário inválido ou ausente.");
            }

            _usuariosServico.AlterarNome(idUsuario, request.NovoNome);

            return Ok(new { mensagem = "Nome alterado com sucesso." });
        }
    }
}
