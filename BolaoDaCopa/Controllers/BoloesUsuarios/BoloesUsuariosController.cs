using BolaoDaCopa.Aplicacao.Boloes.Servicos;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BolaoDaCopa.Controllers.BoloesUsuarios
{
    [Authorize]
    [ApiController]
    [Route("api/boloes-usuarios")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class BoloesUsuariosController : Controller
    {
        private readonly IBoloesUsuariosServico boloesUsuariosServico;
        public BoloesUsuariosController(IBoloesUsuariosServico boloesUsuariosServico)
        {
            this.boloesUsuariosServico = boloesUsuariosServico;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BolaoUsuarioResponse>> ListarBoloesPorUsuario()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim != null && int.TryParse(idClaim.Value, out int idUsuario))
            {
                IEnumerable<BolaoUsuarioResponse>? retorno = boloesUsuariosServico.ListarBoloesPorUsuario(idUsuario);

                return Ok(retorno);
            }
            else
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<BolaoUsuarioResponse> Recuperar(int id)
        {
            BolaoUsuarioResponse retorno = boloesUsuariosServico.Recuperar(id);

            return Ok(retorno);
        }

        /// <summary>
        /// Associar Usuario a um Bolao via hub
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssociarUsuarioBolaoViaHub([FromBody] AssociarBolaoUsuarioViaHubRequest request)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim != null && int.TryParse(idClaim.Value, out int idUsuario))
            {
                request.IdUsuario = idUsuario;
            }
            else
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }

            boloesUsuariosServico.AssociarUsuarioBolaoViaHub(request);
            return Ok();
        }
    }
}
