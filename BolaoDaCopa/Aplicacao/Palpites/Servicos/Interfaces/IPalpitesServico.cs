using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoTeste.Dto;
using BolaoTeste.Dto.ListarPalpite;

namespace BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces
{
    public interface IPalpitesServico
    {
        void CriarPalpiteArtilheiro(CriarPalpiteArtilheiroRequest request);
        Task CriarPalpiteFaseSelecao(CriarPalpiteFaseSelecaoRequest[] request);
        Task CriarPalpiteGrupoSelecao(CriarPalpiteGrupoSelecaoRequest[] request);
        Task CriarPalpiteJogoGrupo(CriarPalpiteJogoGrupoRequest[] request);
    }
}
