using BolaoDaCopa.Bibliotecas.Repositorios;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Models;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.BoloesUsuarios
{
    public class BoloesUsuariosRepositorio : RepositorioNHibernate<BolaoUsuario>, IBoloesUsuariosRepositorio
    {
        private readonly ISession session;

        public BoloesUsuariosRepositorio(ISession session) : base(session)
        {
            this.session = session;
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
