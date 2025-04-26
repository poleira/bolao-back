using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Usuarios
{
    public class UsuariosRepositorio : RepositorioNHibernate<Usuario>, IUsuariosRepositorio
    {

        public UsuariosRepositorio(ISession session) : base(session) { }

    }
}
