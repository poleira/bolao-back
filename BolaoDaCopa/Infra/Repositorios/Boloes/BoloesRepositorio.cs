using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Models;
using MySqlX.XDevAPI;
using NHibernate.Util;
using Ubiety.Dns.Core;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Infra.Repositorios.Boloes
{
    public class BoloesRepositorio : IBoloesRepositorio
    {
        private readonly ISession session;
        
        public BoloesRepositorio(ISession session)
        {
            this.session = session;
        }

        public int Inserir(Bolao bolao)
        {
            var id = (int)session.Save(bolao);
            return id;
        }

        public void InserirTokenAcesso(Bolao bolao, string token)
        {
            bolao.TokenAcesso = token;
            session.Update(bolao);
        }

    }
}
