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

        public async Task DeletarPalpiteGrupoSelecaoPorUsuario(PalpiteGrupoSelecao palpiteGrupoSelecao)
        {
            var palpites = await session.Query<PalpiteGrupoSelecao>()
                .Where(x => x.BolaoUsuario.Id == palpiteGrupoSelecao.BolaoUsuario.Id)
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

        public async Task DeletarPalpiteFaseSelecaoPorUsuario(PalpiteFaseSelecao palpiteFaseSelecao)
        {
            var palpites = await session.Query<PalpiteFaseSelecao>()
                .Where(x => x.BolaoUsuario.Id == palpiteFaseSelecao.BolaoUsuario.Id)
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

        public async Task DeletarPalpiteJogoGrupoPorUsuario(PalpiteJogoGrupo palpiteJogoGrupo)
        {
            var palpites = await session.Query<PalpiteJogoGrupo>()
                .Where(x => x.BolaoUsuario.Id == palpiteJogoGrupo.BolaoUsuario.Id)
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

    }
}
