using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Models;
using Dapper;
using NHibernate;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Boloes
{
    public class BoloesRepositorio : IBoloesRepositorio
    {
        private readonly ISession session;
        
        public BoloesRepositorio(ISession session)
        {
            this.session = session;
        }

        private readonly string sqlDeletarRegrasBolao =
            @"DELETE FROM bolaoregra
            WHERE IDBolao = @idBolao;";

        public int Inserir(Bolao bolao)
        {
            var id = (int)session.Save(bolao);
            return id;
        }
        public void InserirTokenAcesso(Bolao bolao, string token)
        {
            bolao.TokenAcesso = token;
            session.Update(bolao);
        }
        public Bolao Recuperar(int idBolao)
        {
            return session.Get<Bolao>(idBolao);
        }
        public void InserirRegra(BolaoRegra bolaoRegra)
        {
            session.Save(bolaoRegra);
        }
        public void DeletarRegras(int bolaoId)
        {
            DynamicParameters parameters = new();
            parameters.Add("@idBolao", bolaoId);

            session.Connection.Execute(sqlDeletarRegrasBolao, parameters);
        }
        public Regra RecuperarRegra(int idRegra)
        {
            return session.Get<Regra>(idRegra);
        }
        public IQueryable<Regra> QueryRegra()
        {
            session.Clear();
            return session.Query<Regra>();
        }
        public IQueryable<BolaoRegra> QueryBolaoRegra()
        {
            session.Clear();
            return session.Query<BolaoRegra>();
        }

    }
}
