using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Usuarios
{
    public class UsuariosRepositorio : RepositorioNHibernate<Usuario>, IUsuariosRepositorio
    {
        private readonly ISession session;
        public UsuariosRepositorio(ISession session) : base(session) 
        {
            this.session = session;
        }

        public Usuario RecuperarPorHash(string hash)
        {
            return session.Query<Usuario>().FirstOrDefault(x => x.FirebaseUid == hash);
        }

    }
}
