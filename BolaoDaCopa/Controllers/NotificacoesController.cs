using BolaoDaCopa.Aplicacao.Notificacoes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Notificacoes.Requests;
using BolaoDaCopa.Dto.Notificacoes.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BolaoDaCopa.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class NotificacoesController : Controller
    {
        private readonly INotificacoesServico notificacoesServico;
        public NotificacoesController(INotificacoesServico notificacoesServico)
        {
            this.notificacoesServico = notificacoesServico;
        }

        /// <summary>
        /// Lista not. de um usuário logado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<NotificacaoResponse>> ListarNotificacoesPorUsuario()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int idUsuarioLogado;

            if (idClaim != null && int.TryParse(idClaim.Value, out int idUsuario))
            {
                idUsuarioLogado = idUsuario;
            }
            else
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }

            var retorno = notificacoesServico.ListarNotificacoesPorUsuario(idUsuarioLogado);
            return Ok(retorno);
        }

        /// <summary>
        /// Valida se o usuário possui alguma notificação não lida
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("nao-lidas")]
        public ActionResult<bool> ValidarSeUsuarioPossuiAlgumaNotificacaoNaoLida()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int idUsuarioLogado;

            if (idClaim != null && int.TryParse(idClaim.Value, out int idUsuario))
            {
                idUsuarioLogado = idUsuario;
            }
            else
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }

            var retorno = notificacoesServico.ValidarSeUsuarioPossuiAlgumaNotificacaoNaoLida(idUsuarioLogado);
            return Ok(retorno);
        }

        /// <summary>
        /// Marcar uma notificação como lida
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("marcar-como-lida")]
        public ActionResult MarcarNotificacaoComoLida([FromBody] MarcarNotificacaoComoLidaRequest request)
        {
            if (request == null || request.IdNotificacao <= 0)
            {
                return BadRequest("Dados inválidos para marcar notificação como lida.");
            }

            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }

            notificacoesServico.MarcarNotificacaoComoLida(request.IdNotificacao);
            return Ok();
        }

        /// <summary>
        /// Excluir uma notificação específica do usuário logado
        /// </summary>
        /// <param name="idNotificacao"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idNotificacao}")]
        public ActionResult ExcluirNotificacao([FromRoute] int idNotificacao)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }

            notificacoesServico.ExcluirNotificacao(idNotificacao);
            return Ok();
        }

        /// <summary>
        /// Marcar uma notificação como lida
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{idNotificacao}")]
        public ActionResult AceitarSolicitacao([FromRoute] int idNotificacao)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int idUsuarioLogado;

            if (idClaim != null && int.TryParse(idClaim.Value, out int idUsuario))
            {
                idUsuarioLogado = idUsuario;
            }
            else
            {
                throw new UnauthorizedAccessException("ID do usuário inválido ou ausente.");
            }

            notificacoesServico.AceitarSolicitacao(idNotificacao, idUsuarioLogado);
            return Ok();
        }

    }
}
