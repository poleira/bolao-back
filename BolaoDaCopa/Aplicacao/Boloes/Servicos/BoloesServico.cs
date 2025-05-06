using AutoMapper;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;

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

                var usuario = usuariosRepositorio.RecuperarPorHash(inserirRequest.HashUsuario);

                unitOfWork.BeginTransaction();

                var bolao = new Bolao(inserirRequest.Nome, inserirRequest.Logo, inserirRequest.Aviso, inserirRequest.Senha, usuario, inserirRequest.Privado);

                boloesRepositorio.Inserir(bolao);

                string token = CryptoHelper.Encrypt(bolao.Id.ToString());

                bolao.SetTokenAcesso(token);

                boloesRepositorio.Editar(bolao);

                if (inserirRequest.InserirRegrasBoloes.Any())
                {
                    inserirRequest.InserirRegrasBoloes[0].HashBolao = token;
                    InserirRegrasBolao(inserirRequest.InserirRegrasBoloes);
                }

                if (inserirRequest.InserirPremiosBoloes.Any())
                {
                    inserirRequest.InserirPremiosBoloes[0].HashBolao = token;
                    InserirPremiosBolao(inserirRequest.InserirPremiosBoloes);
                }

                unitOfWork.Rollback();

                BolaoResponse? response = mapper.Map<BolaoResponse>(bolao);

                return response;
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception("Erro ao criar o bolão.", ex);
            }
        }

        public Bolao Recuperar(string hashBolao)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                return boloesRepositorio.Recuperar(idBolao);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o bolão.", ex);
            }
        }

        public IList<BolaoResponse> RecuperarBoloesPorUsuario(string hashUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.RecuperarPorHash(hashUsuario) ?? throw new Exception("Usuário não encontrado.");
                var query = boloesUsuariosRepositorio.Query().Where(x => x.Usuario.Id == usuario.Id);
                var projecao = query.Select(x => new BolaoResponse
                {
                    Id = x.Id,
                    Nome = x.Bolao.Nome,
                    Logo = x.Bolao.Logo,
                    TokenAcesso = x.Bolao.TokenAcesso,
                    Aviso = x.Bolao.Aviso,
                    Senha = x.Bolao.Senha
                });

                return projecao.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o bolão usuario.", ex);
            }
        }

        public void AssociarUsuarioBolao(AssociarUsuarioRequest request)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.HashBolao));
                Bolao bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");
                Usuario usuario = usuariosRepositorio.RecuperarPorHash(request.HashUsuario) ?? throw new Exception("Usuário não encontrado.");

                if (request.Senha == bolao.Senha)
                {
                    var comando = new BolaoUsuarioComando(usuario.Id, idBolao);
                    boloesUsuariosRepositorio.Inserir(comando);
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
                var usuarioASerDeletado = usuariosRepositorio.RecuperarPorHash(request.HashUsuario) ?? throw new Exception("Usuário não encontrado.");
                var usuarioLogado = usuariosRepositorio.RecuperarPorHash(request.HashUsuarioLogado) ?? throw new Exception("Usuário logado não encontrado.");

                if (bolao.Usuarios.Any(x => x.Id == usuarioASerDeletado.Id) && (usuarioLogado.Id == usuarioASerDeletado.Id || bolao.UsuarioAdm.Id == usuarioLogado.Id))
                {
                    var comando = new BolaoUsuarioComando(usuarioASerDeletado.Id, idBolao);
                    boloesUsuariosRepositorio.Deletar(comando);
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

        public void InserirRegrasBolao(InserirRegraBolaoRequest[] request)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                var bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");

                boloesRepositorio.DeletarRegrasBolao(idBolao);

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

        public void InserirPremiosBolao(InserirPremioBolaoRequest[] request)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                var bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");

                boloesRepositorio.DeletarPremiosBolao(idBolao);

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

