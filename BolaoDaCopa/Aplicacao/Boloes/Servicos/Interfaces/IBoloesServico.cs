using BolaoDaCopa.Dto.Boloes;
using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Dto.Cadastros;

namespace BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces
{
    public interface IBoloesServico
    {
        ChecarUsuarioResponse Login(ChecarUsuarioRequest usuario);
        IList<CreateCadastroResponse> ListarTodos();
        void CriarBolao(CriarBolaoRequest inserirRequest);

    }
}
