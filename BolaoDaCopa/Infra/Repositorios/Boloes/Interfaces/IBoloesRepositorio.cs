using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces
{
    public interface IBoloesRepositorio
    {
        int Inserir(Bolao bolao);
        void InserirTokenAcesso(Bolao bolao, string token);
        Bolao Recuperar(int idBolao);
        void InserirRegra(BolaoRegra bolaoRegra);
        void DeletarRegras(int bolaoId);
        Regra RecuperarRegra(int idRegra);
        IQueryable<Regra> QueryRegra();
        IQueryable<BolaoRegra> QueryBolaoRegra();
    }
}
