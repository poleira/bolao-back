using BolaoDaCopa.Infra.Repositorios.Palpites.Interface;
using BolaoDaCopa.Models;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Palpites
{
    public class PalpitesRepositorio : IPalpitesRepositorio
    {
        private readonly ISession session;

        public PalpitesRepositorio(ISession session)
        {
            this.session = session;
        }

        public async Task InserirPalpiteArtilheiro(PalpiteArtilheiro palpiteArtilheiro)
        {
            session.Query<PalpiteArtilheiro>()
                   .Where(x => x.BolaoUsuario.Id == palpiteArtilheiro.BolaoUsuario.Id)
                   .ToList()
                   .ForEach(x => session.DeleteAsync(x));

            await session.SaveAsync(palpiteArtilheiro);
        }

        public async Task DeletarPalpiteGrupoSelecaoPorBolaoUsuario(int bolaoUsuarioId)
        {
            var palpites = await session.Query<PalpiteGrupoSelecao>()
                .Where(x => x.BolaoUsuario.Id == bolaoUsuarioId)
                .ToListAsync();

            foreach (var palpite in palpites)
            {
                await session.DeleteAsync(palpite);
            }
        }

        public async Task InserirPalpiteGrupoSelecao(PalpiteGrupoSelecao palpiteGrupoSelecao)
        {
            await session.SaveAsync(palpiteGrupoSelecao);
        }

        public async Task DeletarPalpiteFaseSelecaoPorBolaoUsuario(int bolaoUsuarioId)
        {
            var palpites = await session.Query<PalpiteFaseSelecao>()
                .Where(x => x.BolaoUsuario.Id == bolaoUsuarioId)
                .ToListAsync();

            foreach (var palpite in palpites)
            {
                await session.DeleteAsync(palpite);
            }
        }

        public async Task InserirPalpiteFaseSelecao(PalpiteFaseSelecao palpiteFaseSelecao)
        {
            await session.SaveAsync(palpiteFaseSelecao);
        }

        public async Task DeletarPalpiteJogoGrupoPorBolaoUsuario(int bolaoUsuarioId)
        {
            var palpites = await session.Query<PalpiteJogoGrupo>()
                .Where(x => x.BolaoUsuario.Id == bolaoUsuarioId)
                .ToListAsync();

            foreach (var palpite in palpites)
            {
                await session.DeleteAsync(palpite);
            }
        }

        public async Task InserirPalpiteJogoGrupo(PalpiteJogoGrupo palpiteJogoGrupo)
        {
            await session.SaveAsync(palpiteJogoGrupo);
        }

        public IQueryable<PalpiteArtilheiro> RecuperarQueryPalpiteArtilheiroPorBolaoUsuarioId(int idBolaoUsuario)
        {
            return session.Query<PalpiteArtilheiro>()
                .Where(x => x.BolaoUsuario.Id == idBolaoUsuario);
        }
        public IQueryable<PalpiteFaseSelecao> RecuperarQueryPalpiteFaseSelecaoPorBolaoUsuarioId(int idBolaoUsuario)
        {
            return session.Query<PalpiteFaseSelecao>()
                .Where(x => x.BolaoUsuario.Id == idBolaoUsuario);
        }
        public IQueryable<PalpiteGrupoSelecao> RecuperarQueryPalpiteGrupoSelecaoPorBolaoUsuarioId(int idBolaoUsuario)
        {
            return session.Query<PalpiteGrupoSelecao>()
                .Where(x => x.BolaoUsuario.Id == idBolaoUsuario);
        }
        public IQueryable<PalpiteJogoGrupo> RecuperarQueryPalpiteJogoGrupoPorBolaoUsuarioId(int idBolaoUsuario)
        {
            return session.Query<PalpiteJogoGrupo>()
                .Where(x => x.BolaoUsuario.Id == idBolaoUsuario);
        }
    }
}
