using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Models;
using Dapper;
using NHibernate.Linq;
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

        public async Task<BolaoUsuario> RecuperarAsync(int idBolao, int idUsuario)
        {
            return (await session.Query<BolaoUsuario>().Where(x => x.Bolao.Id == idBolao && x.Usuario.Id == idUsuario).ToListAsync()).FirstOrDefault();
        }

        public IQueryable<BolaoUsuario> Query()
        {
            return session.Query<BolaoUsuario>();
        }
    }
}
