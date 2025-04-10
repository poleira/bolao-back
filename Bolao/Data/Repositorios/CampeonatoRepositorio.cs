using BolaoTeste.Data.Repositorios.Interfaces;
using BolaoTeste.Models;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Data.Repositorios
{
    public class CampeonatoRepositorio : ICampeonatoRepositorio
    {
        private readonly ISession session;

        public CampeonatoRepositorio(ISession session)
        {
            this.session = session;
        }

        public IQueryable<Campeonato> Query()
        {
            session.Clear();
            return session.Query<Campeonato>();
        }
    }
}
