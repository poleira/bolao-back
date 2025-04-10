using BolaoTeste.Data.Interfaces;
using BolaoTeste.Models;
using MySqlX.XDevAPI;
using NHibernate.Util;
using Ubiety.Dns.Core;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Data.Repositorios
{
    public class CadastroRepositorio : ICadastroRepositorio
    {
        private readonly ISession session;
        
        public CadastroRepositorio(ISession session)
        {
            this.session = session;
        }
        public void Deletar(Cadastro cadastro)
        {
            throw new NotImplementedException();
        }

        public Cadastro Editar(Cadastro cadastro)
        {
            session.Merge(cadastro);
            return cadastro;
        }

        public Cadastro Inserir(Cadastro cadastro)
        {
            session.Save(cadastro);
            return cadastro;
        }

        public IQueryable<Cadastro> Query()
        {
            session.Clear();
            return session.Query<Cadastro>();
        }

        public Cadastro Recuperar(int codigo)
        {
            return session.Get<Cadastro>(codigo);
        }

        
        public bool Checar(string usuario)
        {
            var query = Query();

            query.Where(user => user.Usuario == usuario);
            if(query.Count() > 0)
               return true;
            else
                return false;
              
            

        }
    }
}
