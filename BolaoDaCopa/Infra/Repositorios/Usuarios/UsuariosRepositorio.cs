using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Usuarios
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly ISession session;

        public UsuariosRepositorio(ISession session)
        {
            this.session = session;
        }

        //public IQueryable<Campeonato> Query()
        //{
        //    session.Clear();
        //    return session.Query<Campeonato>();
        //}

        public Usuario Recuperar(int id)
        {
            return session.Get<Usuario>(id);
        }
    }
}
