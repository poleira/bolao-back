using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Palpites.Interface
{
    public interface IPalpitesRepositorio
    {
        Task DeletarPalpiteFaseSelecaoPorBolaoUsuario(int bolaoUsuarioId);
        Task DeletarPalpiteGrupoSelecaoPorBolaoUsuario(int bolaoUsuarioId);
        Task DeletarPalpiteJogoGrupoPorBolaoUsuario(int bolaoUsuarioId);
        Task InserirPalpiteArtilheiro(PalpiteArtilheiro palpiteArtilheiro);
        Task InserirPalpiteArtilheiroBrasil(PalpiteArtilheiroBrasil palpiteArtilheiroBrasil);
        Task InserirPalpiteFaseSelecao(PalpiteFaseSelecao palpiteFaseSelecao);
        Task InserirPalpiteGrupoSelecao(PalpiteGrupoSelecao palpiteGrupoSelecao);
        Task InserirPalpiteJogoGrupo(PalpiteJogoGrupo palpiteJogoGrupo);
        Task DeletarPalpiteTerceiroLugarPorBolaoUsuario(int bolaoUsuarioId);
        Task InserirPalpiteTerceiroLugar(PalpiteTerceiroLugar palpiteTerceiroLugar);
        
        IQueryable<PalpiteFaseSelecao> RecuperarQueryPalpiteFaseSelecaoPorBolaoUsuarioId(int idBolaoUsuario);
        IQueryable<PalpiteGrupoSelecao> RecuperarQueryPalpiteGrupoSelecaoPorBolaoUsuarioId(int idBolaoUsuario);
        IQueryable<PalpiteJogoGrupo> RecuperarQueryPalpiteJogoGrupoPorBolaoUsuarioId(int idBolaoUsuario);
        IQueryable<PalpiteArtilheiro> RecuperarQueryPalpiteArtilheiroPorBolaoUsuarioId(int idBolaoUsuario);
        IQueryable<PalpiteArtilheiroBrasil> RecuperarQueryPalpiteArtilheiroBrasilPorBolaoUsuarioId(int idBolaoUsuario);
        IQueryable<PalpiteTerceiroLugar> RecuperarQueryPalpiteTerceiroLugarPorBolaoUsuarioId(int idBolaoUsuario);
    }
}
