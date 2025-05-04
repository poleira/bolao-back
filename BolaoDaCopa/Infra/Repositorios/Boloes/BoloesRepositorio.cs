using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Models;
using Dapper;
using NHibernate;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Boloes
{
    public class BoloesRepositorio : RepositorioNHibernate<Bolao>, IBoloesRepositorio
    {
        private readonly ISession session;
        
        public BoloesRepositorio(ISession session) : base(session) 
        {
            this.session = session;
        }

        private readonly string sqlDeletarRegrasBolao =
            @"DELETE FROM bolaoregra
            WHERE IDBolao = @idBolao;";

        private readonly string sqlDeletarPremiosBolao =
            @"DELETE FROM premio
            WHERE IDBolao = @idBolao;";

        public void InserirRegra(BolaoRegra bolaoRegra)
        {
            session.Save(bolaoRegra);
        }

        public void InserirPremio(Premio premio)
        {
            session.Save(premio);
        }
        public void DeletarRegrasBolao(int bolaoId)
        {
            DynamicParameters parameters = new();
            parameters.Add("@idBolao", bolaoId);

            session.Connection.Execute(sqlDeletarRegrasBolao, parameters);
        }

        public void DeletarPremiosBolao(int bolaoId)
        {
            DynamicParameters parameters = new();
            parameters.Add("@idBolao", bolaoId);

            session.Connection.Execute(sqlDeletarPremiosBolao, parameters);
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
        public IQueryable<Premio> QueryPremio()
        {
            session.Clear();
            return session.Query<Premio>();
        }

    }
}
