
using BolaoTeste.Data.Repositorios.Interfaces;
using BolaoTeste.Models;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Data.Repositorios
{
    public class HabilitarPalpiteRepositorio :IHabilitarPalpiteRepositorio
    {
        private readonly ISession session;

        public HabilitarPalpiteRepositorio(ISession session)
        {
            this.session = session;
        }

        public IQueryable<HabilitarPalpite> Query()
        {
            session.Clear();
            return session.Query<HabilitarPalpite>();
        }
    }
}
