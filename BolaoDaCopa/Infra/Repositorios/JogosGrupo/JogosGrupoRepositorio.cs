using BolaoDaCopa.Infra.Repositorios.JogosGrupo.Interfaces;
using BolaoDaCopa.Models;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.JogosGrupo
{
    public class JogosGrupoRepositorio : IJogosGrupoRepositorio
    {
        private readonly ISession session;

        public JogosGrupoRepositorio(ISession session)
        {
            this.session = session;
        }

        public IQueryable<JogoGrupo> Query()
        {
            session.Clear();
            return session.Query<JogoGrupo>();
        }
    }
}
