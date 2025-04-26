using BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces;
using BolaoDaCopa.Models;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Jogadores
{
    public class JogadoresRepositorio : IJogadoresRepositorio
    {
        private readonly ISession session;
        public JogadoresRepositorio(ISession session)
        {
            this.session = session;
        }
        public IQueryable<Jogador> Query()
        {
            return session.Query<Jogador>();
        }
        public Jogador ObterPorId(int id)
        {
            return session.Get<Jogador>(id);
        }
        public void Adicionar(Jogador jogador)
        {
            session.Save(jogador);
        }
        public void Atualizar(Jogador jogador)
        {
            session.Update(jogador);
        }
        public void Remover(Jogador jogador)
        {
            session.Delete(jogador);
        }
    }
}
