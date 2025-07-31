using BolaoDaCopa.Dto.Regras.Responses;

namespace BolaoDaCopa.Aplicacao.Regras.Servicos.Interfaces
{
    public interface IRegrasServico
    {
        IEnumerable<RegraResponse> Listar();
    }
}
