using AutoMapper;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.BoloesRegras.Responses;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;
using FluentNHibernate.Conventions;

namespace BolaoDaCopa.Aplicacao.Boloes.Servicos
{
    public class BoloesServico : IBoloesServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IBoloesRepositorio boloesRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;

        public BoloesServico(IUnitOfWork unitOfWork, IMapper mapper, IBoloesRepositorio boloesRepositorio, IUsuariosRepositorio usuariosRepositorio, IBoloesUsuariosRepositorio boloesUsuariosRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.boloesRepositorio = boloesRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
        }

        public BolaoResponse CriarBolao(CriarBolaoRequest inserirRequest)
        {
            try
            {
                var query = boloesRepositorio.Query().Where(x => x.Nome == inserirRequest.Nome);

                if (query.Any())
                {
                    throw new Exception("Nome do Bolão já existe.");
                }

                var usuario = usuariosRepositorio.Recuperar(inserirRequest.IdUsuario.Value);

                unitOfWork.BeginTransaction();

                var bolao = new Bolao(inserirRequest.Nome, inserirRequest.Logo, inserirRequest.Aviso, inserirRequest.Senha, usuario, inserirRequest.Privado);

                boloesRepositorio.Inserir(bolao);

                string token = CryptoHelper.Encrypt(bolao.Id.ToString());

                bolao.SetTokenAcesso(token);

                boloesRepositorio.Editar(bolao);

                if (inserirRequest.InserirRegrasBoloes.Any())
                {
                    inserirRequest.InserirRegrasBoloes[0].HashBolao = token;
                    InserirRegrasBolao(inserirRequest.InserirRegrasBoloes, bolao);
                }

                if (inserirRequest.InserirPremiosBoloes.Any())
                {
                    inserirRequest.InserirPremiosBoloes[0].HashBolao = token;
                    InserirPremiosBolao(inserirRequest.InserirPremiosBoloes, bolao);
                }

                BolaoUsuario bolaoUsuario = new BolaoUsuario(usuario, bolao);
                boloesUsuariosRepositorio.Inserir(bolaoUsuario);

                unitOfWork.Commit();

                BolaoResponse? response = mapper.Map<BolaoResponse>(bolao);
                
                return response;
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception("Erro ao criar o bolão.", ex);
            }
        }

        public BolaoResponse EditarBolao(EditarBolaoRequest editarRequest)
        {
            try
            {
                var query = boloesRepositorio.Query().Where(x => x.Nome == editarRequest.Nome);

                if (query.Any())
                {
                    throw new Exception("Nome do Bolão já existe.");
                }

                var usuario = usuariosRepositorio.Recuperar(editarRequest.IdUsuario.Value);

                int idBolao = int.Parse(CryptoHelper.Decrypt(editarRequest.HashBolao));
                Bolao bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");

                if (editarRequest.IdUsuario != bolao.UsuarioAdm.Id)
                {
                    throw new Exception("Usuario não tem permissao para isso.");
                }

                unitOfWork.BeginTransaction();

                bolao.Nome = editarRequest.Nome;
                bolao.Logo = editarRequest.Logo;
                bolao.Aviso = editarRequest.Aviso;
                bolao.Senha = editarRequest.Senha;
                bolao.Privado = editarRequest.Privado;
                bolao.UsuarioAdm = usuario;

                boloesRepositorio.Editar(bolao);

                if (editarRequest.InserirRegrasBoloes.Any())
                {
                    editarRequest.InserirRegrasBoloes[0].HashBolao = editarRequest.HashBolao;
                    InserirRegrasBolao(editarRequest.InserirRegrasBoloes, bolao);
                }

                if (editarRequest.InserirPremiosBoloes.Any())
                {
                    editarRequest.InserirPremiosBoloes[0].HashBolao = editarRequest.HashBolao;
                    InserirPremiosBolao(editarRequest.InserirPremiosBoloes, bolao);
                }

                unitOfWork.Commit();

                BolaoResponse? response = mapper.Map<BolaoResponse>(bolao);

                return response;
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception("Erro ao criar o bolão.", ex);
            }
        }

        public BolaoResponse Recuperar(string hashBolao)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));
                BolaoResponse? response = mapper.Map<BolaoResponse>(boloesRepositorio.Recuperar(idBolao));

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o bolão.", ex);
            }
        }

        public void AssociarUsuarioBolao(AssociarUsuarioRequest request)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.HashBolao));
                Bolao bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");
                Usuario usuario = usuariosRepositorio.Recuperar(request.IdUsuario.Value) ?? throw new Exception("Usuário não encontrado.");

                if (request.Senha == bolao.Senha)
                {
                    var comando = new BolaoUsuarioComando(usuario.Id, idBolao);
                    BolaoUsuario bolaoUsuario = new(usuario, bolao);
                    boloesUsuariosRepositorio.Inserir(bolaoUsuario);
                }
                else
                {
                    throw new Exception("Senha inválida.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao associar o usuário ao bolão.", ex);

            }
        }

        public void DesassociarUsuarioBolao(AssociarUsuarioRequest request)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.HashBolao));
                Bolao bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");
                var usuarioASerDeletado = usuariosRepositorio.Recuperar(request.IdUsuario.Value) ?? throw new Exception("Usuário não encontrado.");
                var usuarioLogado = usuariosRepositorio.RecuperarPorHash(request.HashUsuarioLogado) ?? throw new Exception("Usuário logado não encontrado.");

                if (bolao.Usuarios.Any(x => x.Id == usuarioASerDeletado.Id) && (usuarioLogado.Id == usuarioASerDeletado.Id || bolao.UsuarioAdm.Id == usuarioLogado.Id))
                {
                    BolaoUsuario bolaoUsuario = boloesUsuariosRepositorio.Query().Where(x => x.Usuario.Id == usuarioASerDeletado.Id && x.Bolao.Id == idBolao).FirstOrDefault() ?? throw new Exception("Usuario não encontrado no bolão.");
                    boloesUsuariosRepositorio.Remover(bolaoUsuario);
                }
                else
                {
                    throw new Exception("Usuario nao encontrado no bolão ou te falta permissão.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao desassociar o usuário ao bolão.", ex);

            }
        }

        public void InserirRegrasBolao(InserirRegraBolaoRequest[] request, Bolao? bolaoParametro)
        {
            try
            {
                Bolao bolao = new();
                if (bolaoParametro == null)
                {
                    int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                    bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");
                }
                else
                {
                    bolao = bolaoParametro;
                }

                    boloesRepositorio.DeletarRegrasBolao(bolao.Id);

                foreach (var item in request)
                {
                    var regra = boloesRepositorio.RecuperarRegra(item.IdRegra) ?? throw new Exception("Regra não encontrada.");
                    var bolaoRegra = new BolaoRegra(item.Pontuacao, bolao, regra);
                    boloesRepositorio.InserirRegra(bolaoRegra);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir regras no bolão.", ex);
            }
        }

        public void InserirPremiosBolao(InserirPremioBolaoRequest[] request, Bolao? bolaoParametro)
        {
            try
            {
                Bolao bolao = new();
                if (bolaoParametro == null)
                {
                    int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                    bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");
                }
                else
                {
                    bolao = bolaoParametro;
                }

                boloesRepositorio.DeletarPremiosBolao(bolao.Id);

                foreach (var item in request)
                {
                    var premio = new Premio(item.Descricao, bolao, item.Colocacao);
                    boloesRepositorio.InserirPremio(premio);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir Premios no bolão.", ex);
            }
        }

        public IList<RegraResponse> ListarRegras()
        {
            var query = boloesRepositorio.QueryRegra();

            var response = mapper.Map<IList<RegraResponse>>(query);
            
            return response;
        }

        public IList<BolaoRegraResponse> ListarRegrasBolao(string hashBolao)
        {
            int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));
            var query = boloesRepositorio.QueryBolaoRegra().Where(x => x.Bolao.Id == idBolao);
            var projecao = query.Select(x => new BolaoRegraResponse
            {
                Id = x.Id,
                Descricao = x.Regra.Descricao,
                Explicacao = x.Regra.Explicacao,
                Pontuacao = x.Pontuacao
            });

            return projecao.ToList();
        }

        public IList<PremioResponse> ListarPremiosBolao(string hashBolao)
        {
            int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));
            var query = boloesRepositorio.QueryPremio().Where(x => x.Bolao.Id == idBolao);
            var projecao = query.Select(x => new PremioResponse
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Colocacao = x.Colocacao,
            });

            return projecao.ToList();
        }

    }
}

