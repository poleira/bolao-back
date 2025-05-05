using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces
{
    public interface ISelecoesRepositorio
    {
        //public void EditarGa(GaEditarRequest request, string Idg, string g, string primeiro, string segundo);
        IQueryable<Selecao> QuerySelecao();
        Selecao Recuperar(int id);
        Task<Grupo> RecuperarGrupo(int id);
        IQueryable<Fase> RecuperarQueryFasePorId(int idFase);
    }
}
