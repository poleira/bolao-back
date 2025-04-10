using AutoMapper;
using BolaoTeste.Aplicacao.Cadastros.Servicos.Interfaces;
using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Dto.Cadastros;
using BolaoTeste.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;



namespace BolaoTeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class CadastroController : Controller
    {     

        private readonly ICadastroServico cadastroServico;
        public CadastroController(ICadastroServico cadastroServico)
            {
                this.cadastroServico = cadastroServico;
            }

        [HttpPost]
        public ActionResult AdicionaCadastro([FromBody] CreateCadastroRequest cadastroDto)
        {
            var retorno = cadastroServico.AdicionaCadastro(cadastroDto);
            if (retorno == null)
                return BadRequest("Erro ao tentar salvar, tente novamente mais tarde");
            return Ok(retorno);
        }
        [HttpGet]
        public ActionResult<IList<CreateCadastroResponse>> Listar()
        {
            var retorno = cadastroServico.ListarTodos();
            return Ok(retorno);
        }

        


    }
}

