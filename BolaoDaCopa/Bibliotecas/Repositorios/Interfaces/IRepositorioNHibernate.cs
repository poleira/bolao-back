namespace BolaoDaCopa.Bibliotecas.Repositorios.Interfaces
{
    public interface IRepositorioNHibernate<T> where T : class
    {
        IQueryable<T> Query();
        T Recuperar(int id);
        void Inserir(T entity);
        void Editar(T entidade);
        void Remover(T entity);
    }
}
