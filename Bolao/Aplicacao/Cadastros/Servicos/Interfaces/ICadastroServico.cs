using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Dto.Cadastros;

namespace BolaoTeste.Aplicacao.Cadastros.Servicos.Interfaces
{
    public interface ICadastroServico
    {
        ChecarUsuarioResponse Login(ChecarUsuarioRequest usuario);
        CreateCadastroResponse AdicionaCadastro(CreateCadastroRequest inserirRequest);
        IList<CreateCadastroResponse> ListarTodos();



    }
}
