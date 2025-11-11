using BolaoDaCopa.Bibliotecas.Repositorios.Interfaces;

namespace BolaoDaCopa.Infra.Repositorios.ModoJogoRegra.Interfaces
{
    public interface IModoJogoRegraRepositorio : IRepositorioNHibernate<BolaoDaCopa.Models.ModoJogoRegra>
    {
        IQueryable<BolaoDaCopa.Models.ModoJogoRegra> QueryModoJogoRegra();
    }
}
