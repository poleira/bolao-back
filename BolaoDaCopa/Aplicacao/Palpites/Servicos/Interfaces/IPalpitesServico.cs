using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using BolaoTeste.Dto;
using BolaoDaCopa.Dto.Selecoes.Responses;

namespace BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces
{
    public interface IPalpitesServico
    {
        Task CriarPalpiteArtilheiro(CriarPalpiteArtilheiroRequest request);
        Task CriarPalpiteJogoGrupo(CriarPalpiteJogoGrupoRequest[] request, int idUsuario);
        Task CriarPalpiteGrupoSelecao(CriarPalpiteGrupoSelecaoRequest[] request, int idUsuario);
        Task CriarPalpiteFaseSelecao(CriarPalpiteFaseSelecaoRequest[] request, int idUsuario);
        Task<PalpiteArtilheiroResponse> RecuperarPalpiteArtilheiroAsync(string hashBolao, int idUsuario);
        Task<IList<PalpiteFaseSelecaoResponse>> RecuperarPalpiteFaseSelecaoAsync(string hashBolao, int idUsuario);
        Task<IList<PalpiteGrupoSelecaoResponse>> RecuperarPalpiteGrupoSelecaoAsync(string hashBolao, int idUsuario);
        Task<IList<PalpiteJogoGrupoResponse>> RecuperarPalpiteJogoGrupoAsync(string hashBolao, int idUsuario);
    Task<IList<GrupoSelecaoResponse>> RecuperarPalpiteMelhoresTerceiroLugarAsync(string hashBolao, int idUsuario);
    }
}
