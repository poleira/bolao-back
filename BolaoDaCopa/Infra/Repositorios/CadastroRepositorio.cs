using BolaoTeste.Data.Interfaces;
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

    }
}
