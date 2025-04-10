using BolaoTeste.Models;

namespace BolaoTeste.Data.Repositorios.Interfaces
{
    public interface ICampeonatoRepositorio
    {
        IQueryable<Campeonato> Query();
    }
}
