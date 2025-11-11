using BolaoDaCopa.Infra.Repositorios.ModosJogos.Interfaces;
using BolaoDaCopa.Models;
using NHibernate;

namespace BolaoDaCopa.Infra.Repositorios.ModosJogos
{
    public class ModosJogosRepositorios : RepositorioNHibernate<ModoJogo>, IModosJogosRepositorios
    {
        public ModosJogosRepositorios(ISession session) : base(session)
        {
        }
    }
}
