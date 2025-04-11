using AutoMapper;
using BolaoTeste.Aplicacao.Cadastros.Servicos.Interfaces;
using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Data.Interfaces;

using ISession = NHibernate.ISession;
using BolaoTeste.Dto.Cadastros;
using BolaoTeste.Services;
using System.Net.Http.Headers;

namespace BolaoTeste.Aplicacao.Cadastros.Servicos
{
    public class CadastroServico : ICadastroServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly ICadastroRepositorio cadastroRepositorio;

        public CadastroServico( ISession session, IMapper mapper, ICadastroRepositorio cadastroRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.cadastroRepositorio = cadastroRepositorio;
        }

        public CreateCadastroResponse AdicionaCadastro(CreateCadastroRequest inserirRequest)
        {
           

            var transacao = session.BeginTransaction();            
            try
            {
                if (transacao.IsActive)
                    transacao.Commit();
                return null;
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public IList<CreateCadastroResponse> ListarTodos()
        {
            try
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        public ChecarUsuarioResponse Login(ChecarUsuarioRequest usuario)
        {
            try
            {
                return null;

                throw new Exception("Usuario nao encontrado");

            }
            catch
            {
                return null;
            }


        }


    }
}

