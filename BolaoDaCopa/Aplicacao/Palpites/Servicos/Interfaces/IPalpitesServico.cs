using BolaoTeste.Dto;
using BolaoTeste.Dto.JogosBr;
using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Palpites;

namespace BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces
{
    public interface IPalpitesServico
    {
        OkResponse EditaCampeao(CampeaoEditarRequest request);

    }
}
