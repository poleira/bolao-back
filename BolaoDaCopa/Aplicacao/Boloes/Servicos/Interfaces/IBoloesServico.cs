using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.BoloesRegras.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces
{
    public interface IBoloesServico
    {
        BolaoResponse CriarBolao(CriarBolaoRequest inserirRequest);
        void AssociarUsuarioBolao(AssociarUsuarioRequest request);
        void InserirRegrasBolao(InserirRegraBolaoRequest[] request, Bolao? bolao);
        IList<RegraResponse> ListarRegras();
        IList<BolaoRegraResponse> ListarRegrasBolao(string hashBolao);
        void DesassociarUsuarioBolao(AssociarUsuarioRequest request);
        IList<PremioResponse> ListarPremiosBolao(string hashBolao);
        BolaoResponse EditarBolao(EditarBolaoRequest editarRequest);
        BolaoResponse Recuperar(string hashBolao);
        void InserirPremiosBolao(InserirPremioBolaoRequest[] request, Bolao? bolaoParametro);
    }
}
