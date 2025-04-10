using AutoMapper;
using BolaoTeste.Aplicacao.Cadastros.Servicos.Interfaces;
using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Data.Interfaces;

using ISession = NHibernate.ISession;
using BolaoTeste.Models;
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
            var ga = new Ga();
            var gb = new Gb();
            var gc = new Gc();
            var gd = new Gd();
            var ge = new Ge();
            var gf = new Gf();
            var gg = new Gg();
            var gh = new Gh();
            var oitavas = new Oitavas();
            var quartas = new Quartas();
            var semis = new Semis();
            var finais = new Finais();
            var jogosDoBr = new Jogos_BR();
            var campeao = new Campeao();
            Cadastro cadastroInserir = mapper.Map<Cadastro>(inserirRequest);
            cadastroInserir.Ga = ga;
            cadastroInserir.Gb = gb;
            cadastroInserir.Gc = gc;
            cadastroInserir.Gd = gd;
            cadastroInserir.Ge = ge;
            cadastroInserir.Gf = gf;
            cadastroInserir.Gg = gg;
            cadastroInserir.Gh = gh;
            cadastroInserir.Oitavas = oitavas;
            cadastroInserir.Quartas = quartas;
            cadastroInserir.Semis = semis;
            cadastroInserir.Finais = finais;
            cadastroInserir.Jogos_BR = jogosDoBr;
            cadastroInserir.Campeao = campeao;

            var transacao = session.BeginTransaction();            
            try
            {                
                IList<Cadastro> cadastroDb = cadastroRepositorio.Query().ToList();
                foreach (var item in cadastroDb)
                {
                    if(item.Usuario == inserirRequest.Usuario)
                    {
                        throw new Exception("Usuario ja existente");
                    }
                }
                cadastroInserir = cadastroRepositorio.Inserir(cadastroInserir);
                if (transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<CreateCadastroResponse>(cadastroInserir);
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
                IList<Cadastro> categoriasDb = cadastroRepositorio.Query().ToList();             
                IList<CreateCadastroResponse> categorias = mapper.Map<IList<CreateCadastroResponse>>(categoriasDb);
                return categorias;
            }
            catch
            {
                return null;
            }
        }

        public ChecarUsuarioResponse Login(ChecarUsuarioRequest usuario)
        {
            var login = mapper.Map<Cadastro>(usuario);
            IList<Cadastro> cadastros = cadastroRepositorio.Query().ToList();
            try
            {
                foreach (var c in cadastros)
                {
                    if (c.Usuario == login.Usuario && c.Senha == login.Senha)
                    {
                        var token = TokenService.GenerateToken(login);

                        var retorno = new ChecarUsuarioResponse();
                        retorno.Token = token;
                        retorno.Id = c.Id;

                        return retorno;
                    }                        
                }
  
                throw new Exception("Usuario nao encontrado");

            }
            catch
            {
                return null;
            }


        }


    }
}

