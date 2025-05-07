using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.BoloesRegras.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces
{
    public interface IBoloesServico
    {
        BolaoResponse CriarBolao(CriarBolaoRequest inserirRequest);
        Bolao Recuperar(string hashBolao);
        void AssociarUsuarioBolao(AssociarUsuarioRequest request);
        void InserirRegrasBolao(InserirRegraBolaoRequest[] request);
        IList<RegraResponse> ListarRegras();
        IList<BolaoRegraResponse> ListarRegrasBolao(string hashBolao);
        void DesassociarUsuarioBolao(AssociarUsuarioRequest request);
        void InserirPremiosBolao(InserirPremioBolaoRequest[] request);
        IList<PremioResponse> ListarPremiosBolao(string hashBolao);
    }
}
