using BolaoDaCopa.Bibliotecas.Repositorios.Interfaces;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces
{
    public interface IUsuariosRepositorio : IRepositorioNHibernate<Usuario>
    {
        Usuario RecuperarPorHash(string hash);
    }
}
