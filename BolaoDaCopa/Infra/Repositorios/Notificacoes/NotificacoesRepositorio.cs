using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.Notificacoes.Interfaces;
using BolaoDaCopa.Models;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Notificacoes
{
    public class NotificacoesRepositorio : RepositorioNHibernate<Notificacao>, INotificacoesRepositorio
    {
        private readonly ISession session;
        public NotificacoesRepositorio(ISession session) : base(session)
        {
            this.session = session;
        }
    }
}
