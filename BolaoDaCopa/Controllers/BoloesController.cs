using AutoMapper;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes;
using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Dto.Cadastros;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;



namespace BolaoDaCopa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class BoloesController : Controller
    {     
        private readonly IBoloesServico cadastroServico;
        public BoloesController(IBoloesServico cadastroServico)
        {
            this.cadastroServico = cadastroServico;
        }

        [HttpPost]
        public ActionResult CriarBolao([FromBody] CriarBolaoRequest request)
        {
            cadastroServico.CriarBolao(request);
            return Ok();
        }
        [HttpGet]
        public ActionResult<IList<CreateCadastroResponse>> Listar()
        {
            var retorno = cadastroServico.ListarTodos();
            return Ok(retorno);
        }
    }
}

