using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.ModoJogoRegra.Interfaces;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.ModoJogoRegra
{
    public class ModoJogoRegraRepositorio : RepositorioNHibernate<BolaoDaCopa.Models.ModoJogoRegra>, IModoJogoRegraRepositorio
    {
        private readonly ISession _session;

        public ModoJogoRegraRepositorio(ISession session) : base(session)
        {
            _session = session;
        }

        public IQueryable<BolaoDaCopa.Models.ModoJogoRegra> QueryModoJogoRegra()
        {
            session.Clear();
            return session.Query<BolaoDaCopa.Models.ModoJogoRegra>();
        }
    }
}
