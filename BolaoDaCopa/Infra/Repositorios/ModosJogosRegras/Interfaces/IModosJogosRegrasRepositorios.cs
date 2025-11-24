using BolaoDaCopa.Bibliotecas.Repositorios.Interfaces;

namespace BolaoDaCopa.Infra.Repositorios.ModosJogosRegras.Interfaces
{
    public interface IModosJogosRegrasRepositorios : IRepositorioNHibernate<BolaoDaCopa.Models.ModoJogoRegra>
    {
        IQueryable<BolaoDaCopa.Models.ModoJogoRegra> QueryModosJogosRegras();
    }
}
