using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces
{
    public interface IBoloesRepositorio
    {
        int Inserir(Bolao bolao);
        void InserirTokenAcesso(Bolao bolao, string token);
    }
}
