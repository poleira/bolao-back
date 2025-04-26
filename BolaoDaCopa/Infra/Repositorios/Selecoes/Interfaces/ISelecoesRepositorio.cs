using BolaoDaCopa.Models;
namespace BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces
{
    public interface ISelecoesRepositorio
    {
        //public void EditarGa(GaEditarRequest request, string Idg, string g, string primeiro, string segundo);
        IQueryable<GrupoSelecao> QueryGrupoSelecao();
        IQueryable<Selecao> QuerySelecao();
        Selecao Recuperar(int id);
    }
}
