using BolaoDaCopa.Infra.Repositorios.ModoJogo.Interfaces;
using BolaoDaCopa.Models;
using NHibernate;

namespace BolaoDaCopa.Infra.Repositorios.ModoJogo
{
    public class ModoJogoRepositorio : RepositorioNHibernate<ModoJogo>, IModoJogoRepositorio
    {
        public ModoJogoRepositorio(ISession session) : base(session)
        {
        }
    }
}
