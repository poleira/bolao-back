using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.JogosGrupo.Interfaces
{
    public interface IJogosGrupoRepositorio
    {
        IQueryable<JogoGrupo> Query();
    }
}
