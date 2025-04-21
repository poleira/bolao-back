using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces
{
    public interface IBoloesServico
    {
        void CriarBolao(CriarBolaoRequest inserirRequest);
        Bolao Recuperar(string hashBolao);
        void AssociarUsuarioBolao(AssociarUsuarioRequest request);
        void InserirRegrasBolao(InserirRegrasBolaoRequest[] request);
        IList<Regra> ListarRegras();
        IList<BolaoRegraResponse> ListarRegrasBolao(string hashBolao);
        void DesassociarUsuarioBolao(AssociarUsuarioRequest request);
    }
}
