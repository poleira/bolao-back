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
            @"DELETE FROM bolao.bolaoregra
            WHERE idbolao = @idBolao;";

        private readonly string sqlDeletarPremiosBolao =
            @"DELETE FROM bolao.premio
            WHERE idbolao = @idBolao;";

        private readonly string sqlDeletarPalpitesBolao =
            @"DELETE FROM bolao.palpiteartilheiro      WHERE idbolaousuario IN (SELECT id FROM bolao.bolaousuario WHERE idbolao = @idBolao);
              DELETE FROM bolao.palpiteartilheirobrasil WHERE idbolaousuario IN (SELECT id FROM bolao.bolaousuario WHERE idbolao = @idBolao);
              DELETE FROM bolao.palpitefaseselecao      WHERE idbolaousuario IN (SELECT id FROM bolao.bolaousuario WHERE idbolao = @idBolao);
              DELETE FROM bolao.palpitegruposelecao     WHERE idbolaousuario IN (SELECT id FROM bolao.bolaousuario WHERE idbolao = @idBolao);
              DELETE FROM bolao.palpitejogogrupo        WHERE idbolaousuario IN (SELECT id FROM bolao.bolaousuario WHERE idbolao = @idBolao);
              DELETE FROM bolao.palpiteterceirolugar    WHERE idbolaousuario IN (SELECT id FROM bolao.bolaousuario WHERE idbolao = @idBolao);
              DELETE FROM bolao.bolaousuario            WHERE idbolao = @idBolao;";

        private readonly string sqlDeletarBolao =
            @"DELETE FROM bolao.bolao WHERE id = @idBolao;";

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

        public void DeletarBolao(int bolaoId)
        {
            DynamicParameters parameters = new();
            parameters.Add("@idBolao", bolaoId);

            session.Connection.Execute(sqlDeletarRegrasBolao, parameters);
            session.Connection.Execute(sqlDeletarPremiosBolao, parameters);
            session.Connection.Execute(sqlDeletarPalpitesBolao, parameters);
            session.Connection.Execute(sqlDeletarBolao, parameters);
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
