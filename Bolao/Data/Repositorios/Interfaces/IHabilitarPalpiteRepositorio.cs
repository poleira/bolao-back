using BolaoTeste.Models;

namespace BolaoTeste.Data.Repositorios.Interfaces
{
    public interface IHabilitarPalpiteRepositorio
    {
        IQueryable<HabilitarPalpite> Query();
    }
}
