using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Usuario Recuperar(int id);
    }
}
