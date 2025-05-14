using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<BolaoUsuarioResponse>> ListarBoloesUsuario([FromQuery] string hashUsuario)
        {
            IEnumerable<BolaoUsuarioResponse>? retorno = boloesUsuariosServico.ListarBoloesUsuario(hashUsuario);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<BolaoUsuarioResponse> Recuperar(int id)
        {
            BolaoUsuarioResponse retorno = boloesUsuariosServico.Recuperar(id);

            return Ok(retorno);
        }
    }
}
