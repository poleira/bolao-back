using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Models;
using Dapper;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.BoloesUsuarios
{
    public class BoloesUsuariosRepositorio : IBoloesUsuariosRepositorio
    {
        private readonly ISession session;

        public BoloesUsuariosRepositorio(ISession session)
        {
            this.session = session;
        }

        private readonly string sqlInserir =
            @"INSERT INTO bolaousuario (IDUsuario, IDBolao)
            VALUES (@idUsuario, @idBolao);";

        private readonly string sqlDeletar =
            @"DELETE FROM bolaousuario
            WHERE IDBolao = @idBolao AND 
            IDUsuario = @idUsuario;";

        public void Inserir(BolaoUsuarioComando bolaoUsuario)
        {
            DynamicParameters parameters = new();
            parameters.Add("@idUsuario", bolaoUsuario.IdUsuario);
            parameters.Add("@idBolao", bolaoUsuario.IdBolao);

            session.Connection.Execute(sqlInserir, parameters);
        }

        public void Deletar(BolaoUsuarioComando bolaoUsuario)
        {
            DynamicParameters parameters = new();
            parameters.Add("@idUsuario", bolaoUsuario.IdUsuario);
            parameters.Add("@idBolao", bolaoUsuario.IdBolao);
            session.Connection.Execute(sqlDeletar, parameters);
        }

        public BolaoUsuario Recuperar(int idBolao, int idUsuario)
        {
            DynamicParameters parameters = new();
            parameters.Add("@idBolao", idBolao);
            parameters.Add("@idUsuario", idUsuario);
            return session.Connection.QuerySingleOrDefault<BolaoUsuario>(@"SELECT * FROM bolaousuario WHERE IDBolao = @idBolao AND IDUsuario = @idUsuario", parameters);
        }
    }
}
