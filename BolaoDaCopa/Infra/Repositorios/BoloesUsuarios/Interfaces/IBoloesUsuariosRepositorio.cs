using BolaoDaCopa.Bibliotecas.Repositorios.Interfaces;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces
{
    public interface IBoloesUsuariosRepositorio : IRepositorioNHibernate<BolaoUsuario>
    {
        Task<BolaoUsuario> RecuperarAsync(int idBolao, int idUsuario);
        IQueryable<BolaoUsuario> Query();
    }
}
