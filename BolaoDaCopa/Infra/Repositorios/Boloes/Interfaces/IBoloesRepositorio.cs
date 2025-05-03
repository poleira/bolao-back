using BolaoDaCopa.Bibliotecas.Repositorios.Interfaces;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces
{
    public interface IBoloesRepositorio : IRepositorioNHibernate<Bolao>
    {
        void InserirRegra(BolaoRegra bolaoRegra);
        void DeletarRegrasBolao(int bolaoId);
        Regra RecuperarRegra(int idRegra);
        IQueryable<Regra> QueryRegra();
        IQueryable<BolaoRegra> QueryBolaoRegra();
        void DeletarPremiosBolao(int bolaoId);
        void InserirPremio(Premio premio);
        IQueryable<Premio> QueryPremio();
    }
}
