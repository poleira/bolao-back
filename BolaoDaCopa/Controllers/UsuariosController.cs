using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.Usuarios.Requests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Mapping;
using System.Net;

namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServico _usuariosServico;
        public UsuariosController(IUsuariosServico _usuariosServico)
        {
            this._usuariosServico = _usuariosServico;
        }

        [HttpPost]
        public ActionResult Inserir([FromBody] UsuarioRequest request)
        {
            var response = _usuariosServico.Inserir(request);

            return Ok(response);
        }
    }
}
