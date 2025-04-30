using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Palpites.Interface
{
    public interface IPalpitesRepositorio
    {
        Task DeletarPalpiteFaseSelecaoPorUsuario(PalpiteFaseSelecao palpiteFaseSelecao);
        Task DeletarPalpiteGrupoSelecaoPorUsuario(PalpiteGrupoSelecao palpiteGrupoSelecao);
        Task DeletarPalpiteJogoGrupoPorUsuario(PalpiteJogoGrupo palpiteJogoGrupo);
        Task InserirPalpiteArtilheiro(PalpiteArtilheiro palpiteArtilheiro);
        Task InserirPalpiteFaseSelecao(PalpiteFaseSelecao palpiteFaseSelecao);
        Task InserirPalpiteGrupoSelecao(PalpiteGrupoSelecao palpiteGrupoSelecao);
        Task InserirPalpiteJogoGrupo(PalpiteJogoGrupo palpiteJogoGrupo);
    }
}
