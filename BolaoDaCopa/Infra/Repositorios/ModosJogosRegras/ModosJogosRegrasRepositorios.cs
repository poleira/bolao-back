using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.ModosJogosRegras.Interfaces;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.ModosJogosRegras
{
    public class ModosJogosRegrasRepositorios : RepositorioNHibernate<BolaoDaCopa.Models.ModoJogoRegra>, IModosJogosRegrasRepositorios
    {
        private readonly ISession _session;

        public ModosJogosRegrasRepositorios(ISession session) : base(session)
        {
            _session = session;
        }

        public IQueryable<BolaoDaCopa.Models.ModoJogoRegra> QueryModosJogosRegras()
        {
            session.Clear();
            return session.Query<BolaoDaCopa.Models.ModoJogoRegra>();
        }
    }
}
