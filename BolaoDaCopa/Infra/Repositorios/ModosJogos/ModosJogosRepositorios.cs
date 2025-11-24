using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.ModosJogos.Interfaces;
using BolaoDaCopa.Models;
using NHibernate;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.ModosJogos
{
    public class ModosJogosRepositorios : RepositorioNHibernate<BolaoDaCopa.Models.ModoJogo>, IModosJogosRepositorios
    {
        private readonly ISession _session;

        public ModosJogosRepositorios(ISession session) : base(session)
        {
            _session = session;
        }
    }
}
