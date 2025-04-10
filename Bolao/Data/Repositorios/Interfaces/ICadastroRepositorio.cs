using BolaoTeste.Models;

namespace BolaoTeste.Data.Interfaces
{
    public interface ICadastroRepositorio
    {
        Cadastro Recuperar(int codigo);
        Cadastro Inserir(Cadastro cadastro);
        Cadastro Editar(Cadastro cadastro);
        bool Checar(string Usuario);
        void Deletar(Cadastro cadastro);
        IQueryable<Cadastro> Query();
    }
}
