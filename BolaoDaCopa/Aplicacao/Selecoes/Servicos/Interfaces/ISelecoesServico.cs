using BolaoDaCopa.Dto.Selecoes.Requests;
using BolaoDaCopa.Dto.Selecoes.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces
{
    public interface ISelecoesServico
    {
        IList<GrupoSelecaoResponse> ListarSelecoes(GrupoSelecaoRequest request);
    }
}
