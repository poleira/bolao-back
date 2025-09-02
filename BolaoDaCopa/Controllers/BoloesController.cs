using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.BoloesRegras.Responses;
using BolaoDaCopa.Dto.Regras.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BolaoDaCopa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/boloes")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class BoloesController : Controller
    {
        private readonly IBoloesServico boloesServico;
        public BoloesController(IBoloesServico boloesServico)
        {
            this.boloesServico = boloesServico;
        }

        /// <summary>
        /// Cria Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BolaoResponse> CriarBolao([FromBody] CriarBolaoRequest request)
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

            BolaoResponse? response = boloesServico.CriarBolao(request);

            return Ok(response);
        }

        /// <summary>
        /// Edita Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult EditarBolao([FromBody] EditarBolaoRequest request)
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

            boloesServico.EditarBolao(request);
            return Ok();
        }

        /// <summary>
        /// Recupera info Bolao
        /// </summary>
        /// <param name="hashBolao"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{hashBolao}")]
        public ActionResult<BolaoResponse> Recuperar(string hashBolao)
        {
            var retorno = boloesServico.Recuperar(hashBolao);
            return Ok(retorno);
        }

        /// <summary>
        /// Associar Usuario a um Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("boloes-usuarios")]
        public ActionResult AssociarUsuarioBolao([FromBody] AssociarUsuarioRequest request)
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

            boloesServico.AssociarUsuarioBolao(request);
            return Ok();
        }

        /// <summary>
        /// Desassociar Usuario a um Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("usuarios")]
        public ActionResult DesassociarUsuarioBolao([FromBody] AssociarUsuarioRequest request)
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
            boloesServico.DesassociarUsuarioBolao(request);
            return Ok();
        }

        //Nao esta sendo usado
        ///// <summary>
        ///// Inserir regras a um Bolao
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("regras-bolao")]
        //public ActionResult InserirRegrasBolao([FromBody] InserirRegraBolaoRequest[] request)
        //{
        //    var idClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
        //    boloesServico.InserirRegrasBolao(request, null);
        //    return Ok();
        //}

        /// <summary>
        /// Listar Regras de um Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("regras-boloes")]
        public ActionResult<IList<BolaoRegraResponse>> ListarRegrasBolao([FromQuery] HashBolaoRequest request)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            var retorno = boloesServico.ListarRegrasBolao(request.HashBolao);
            return Ok(retorno);
        }

        /// <summary>
        /// Listar Regras de um Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("premios-boloes")]
        public ActionResult<IList<PremioResponse>> ListarPremiosBolao([FromQuery] HashBolaoRequest request)
        {
            var retorno = boloesServico.ListarPremiosBolao(request.HashBolao);
            return Ok(retorno);
        }

        /// <summary>
        /// Listar Boloes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IList<BolaoListarResponse>> ListarBoloes([FromQuery] BolaoListarRequest request)
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
            var retorno = boloesServico.ListarBoloes(request);
            return Ok(retorno);
        }

        /// <summary>
        /// Inserir premios a um Bolao
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("premios-bolao")]
        public ActionResult InserirPremiosBolao([FromBody] InserirPremioBolaoRequest[] request)
        {
            boloesServico.InserirPremiosBolao(request, null);
            return Ok();
        }

    }
}

