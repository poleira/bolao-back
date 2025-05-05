using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using BolaoTeste.Dto;

namespace BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces
{
    public interface IPalpitesServico
    {
        Task CriarPalpiteFaseSelecao(CriarPalpiteFaseSelecaoRequest[] request);
        Task CriarPalpiteGrupoSelecao(CriarPalpiteGrupoSelecaoRequest[] request);
        Task CriarPalpiteJogoGrupo(CriarPalpiteJogoGrupoRequest[] request);
        Task CriarPalpiteArtilheiro(CriarPalpiteArtilheiroRequest request);
        Task<PalpiteArtilheiroResponse> RecuperarPalpiteArtilheiroAsync(string hashBolao, string hashUsuario);
        Task<IList<PalpiteFaseSelecaoResponse>> RecuperarPalpiteFaseSelecaoAsync(string hashBolao, string hashUsuario);
        Task<IList<PalpiteGrupoSelecaoResponse>> RecuperarPalpiteGrupoSelecaoAsync(string hashBolao, string hashUsuario);
        Task<IList<PalpiteJogoGrupoResponse>> RecuperarPalpiteJogoGrupoAsync(string hashBolao, string hashUsuario);
    }
}
