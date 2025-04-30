using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces
{
    public interface IJogadoresRepositorio
    {
        IQueryable<Jogador> Query();
        Jogador Recuperar(int id);
        void Adicionar(Jogador jogador);
        void Atualizar(Jogador jogador);
        void Remover(Jogador jogador);
    }
}
