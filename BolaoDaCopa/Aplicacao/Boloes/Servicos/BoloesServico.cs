using AutoMapper;
using BolaoTeste.Data.Dto.Cadastros;

using ISession = NHibernate.ISession;
using BolaoTeste.Dto.Cadastros;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Dto.Boloes;
using BolaoDaCopa.Models;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Services;


namespace BolaoDaCopa.Aplicacao.Boloes.Servicos
{
    public class BoloesServico : IBoloesServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly IBoloesRepositorio boloesRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;

        public BoloesServico(ISession session, IMapper mapper, IBoloesRepositorio boloesRepositorio, IUsuariosRepositorio usuariosRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.boloesRepositorio = boloesRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
        }

        public void CriarBolao(CriarBolaoRequest inserirRequest)
        {
            var transacao = session.BeginTransaction();            
            try
            {
                var usuario = usuariosRepositorio.Recuperar(inserirRequest.IdUsuario);
                var bolao = new Bolao(inserirRequest.Nome, inserirRequest.Logo, inserirRequest.Aviso, inserirRequest.Senha, usuario);
                bolao.Id = boloesRepositorio.Inserir(bolao);
                var token = CryptoHelper.Encrypt(bolao.Id.ToString());
                boloesRepositorio.InserirTokenAcesso(bolao, token);

                if (transacao.IsActive)
                    transacao.Commit();
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
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

