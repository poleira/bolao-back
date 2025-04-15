using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoTeste.Dto.Cadastros;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Mapping;
using System.Net;



namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class LoginController : Controller
    {
        private readonly IBoloesServico cadastroServico;
        public LoginController(IBoloesServico cadastroServico)
        {
            this.cadastroServico = cadastroServico;
        }

        [HttpPost]
        public ActionResult Login([FromBody] ChecarUsuarioRequest loginDto)
        {
 
            var retorno = cadastroServico.Login(loginDto);
            
            if (retorno == null) 
            {
                return Unauthorized();
            }
            return Ok(retorno);
            

            
        }

        



    }
}
