using BolaoDaCopa.Bibliotecas.Repositorios.Interfaces;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Bibliotecas.Repositorios
{
    public class RepositorioNHibernate<T> : IRepositorioNHibernate<T> where T : class
    {
        protected readonly ISession session;

        public RepositorioNHibernate(ISession session)
        {
            this.session = session;
        }

        public IQueryable<T> Query()
        {
            return session.Query<T>();
        }

        public void Inserir(T entity)
        {
            session.Save(entity);
        }

        //public PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd)
        //{
        //    var resultado = new PaginacaoConsulta<T>();

        //    try
        //    {
        //        query = query.OrderBy(cpOrd + " " + tpOrd.ToString());

        //        // Aplicar a paginação aos resultados
        //        resultado.Registros = query.Skip((pg - 1) * qt).Take(qt).ToList();

        //        // Obter o número total de registros sem aplicar a paginação
        //        resultado.Total = query.Count();

        //        return resultado;
        //    }
        //    catch (ParseException ex)
        //    {
        //        throw ex;
        //    }
        //}

        public T Recuperar(int id)
        {
            return session.Get<T>(id);
        }

        public void Remover(T entity)
        {
            session.Delete(entity);
        }

        public void Editar(T entidade)
        {
            session.Update(entidade);
        }
    }
}
