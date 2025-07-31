using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.Regras.Interfaces;
using BolaoDaCopa.Models;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Regras
{
    public class RegrasRepositorio : RepositorioNHibernate<Regra>, IRegrasRepositorio
    {
        private readonly ISession _session;

        public RegrasRepositorio(ISession session) : base(session)
        {
            this._session = session;
        }
        
        public Regra RecuperarRegra(int idRegra)
        {
            return session.Get<Regra>(idRegra);
        }

        public IQueryable<Regra> QueryRegra()
        {
            session.Clear();

            return session.Query<Regra>();
        }
    }
}
