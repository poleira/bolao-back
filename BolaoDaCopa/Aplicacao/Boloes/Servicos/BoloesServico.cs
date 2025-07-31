using System.Web;
using AutoMapper;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.BoloesRegras.Responses;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;
using BolaoDaCopa.Dto.Regras.Responses;
using BolaoDaCopa.Dto.Usuarios;
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
                var query = boloesRepositorio.Query()
                    .Where(x => x.TokenAcesso != editarRequest.HashBolao)
                    .Where(x => x.Nome == editarRequest.Nome);

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

                return ConstruirResponse(bolao);
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
                string hash = HttpUtility.UrlDecode(hashBolao).Replace(" ", "+");

                int idBolao = int.Parse(CryptoHelper.Decrypt(hash));

                Bolao bolao = boloesRepositorio.Recuperar(idBolao);

                return ConstruirResponse(bolao);
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

                bool usuarioJaAssociado = boloesUsuariosRepositorio.Query()
                    .Any(x => x.Bolao.Id == idBolao && x.Usuario.Id == usuario.Id);

                unitOfWork.BeginTransaction();

                if (usuarioJaAssociado)
                {
                    throw new Exception("Usuário já está associado a este bolão.");
                }

                if (request.Senha == bolao.Senha)
                {
                    var comando = new BolaoUsuarioComando(usuario.Id, idBolao);
                    BolaoUsuario bolaoUsuario = new(usuario, bolao);
                    boloesUsuariosRepositorio.Inserir(bolaoUsuario);
                }
                else
                {
                    unitOfWork.Rollback();
                    throw new Exception("Senha inválida.");
                }

                unitOfWork.Commit();
            }
            catch(Exception)
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public void DesassociarUsuarioBolao(AssociarUsuarioRequest request)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.HashBolao));
                Bolao bolao = boloesRepositorio.Recuperar(idBolao) ?? throw new Exception("Bolão não encontrado.");
                var usuarioASerDeletado = usuariosRepositorio.RecuperarPorHash(request.HashUsuarioASerDeletado) ?? throw new Exception("Usuário não encontrado.");
                var usuarioLogado = usuariosRepositorio.Recuperar(request.IdUsuario.Value) ?? throw new Exception("Usuário logado não encontrado.");

                unitOfWork.BeginTransaction();

                if (bolao.Usuarios.Any(x => x.Id == usuarioASerDeletado.Id) && (usuarioLogado.Id == usuarioASerDeletado.Id || bolao.UsuarioAdm.Id == usuarioLogado.Id))
                {
                    BolaoUsuario bolaoUsuario = boloesUsuariosRepositorio.Query().Where(x => x.Usuario.Id == usuarioASerDeletado.Id && x.Bolao.Id == idBolao).FirstOrDefault() ?? throw new Exception("Usuario não encontrado no bolão.");
                    boloesUsuariosRepositorio.Remover(bolaoUsuario);
                }
                else
                {
                    unitOfWork.Rollback();
                    throw new Exception("Usuario nao encontrado no bolão ou te falta permissão.");
                }

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
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

                unitOfWork.BeginTransaction();

                boloesRepositorio.DeletarRegrasBolao(bolao.Id);

                foreach (var item in request)
                {
                    var regra = boloesRepositorio.RecuperarRegra(item.IdRegra) ?? throw new Exception("Regra não encontrada.");
                    var bolaoRegra = new BolaoRegra(item.Pontuacao, bolao, regra);
                    boloesRepositorio.InserirRegra(bolaoRegra);
                }

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
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

                unitOfWork.BeginTransaction();

                boloesRepositorio.DeletarPremiosBolao(bolao.Id);

                foreach (var item in request)
                {
                    var premio = new Premio(item.Descricao, bolao, item.Colocacao);
                    boloesRepositorio.InserirPremio(premio);
                }

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception("Erro ao inserir Premios no bolão.", ex);
            }
        }

        public IList<BolaoRegraResponse> ListarRegrasBolao(string hashBolao)
        {
            int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));
            var query = boloesRepositorio.QueryBolaoRegra().Where(x => x.Bolao.Id == idBolao);
            var projecao = query.Select(x => new BolaoRegraResponse
            {
                Id = x.Id,
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

        public IList<BolaoListarResponse> ListarBoloes(BolaoListarRequest request)
        {
            var query = boloesRepositorio.Query();

            if (!string.IsNullOrEmpty(request.Nome))
            {
                query = query.Where(x => x.Nome.Contains(request.Nome));
            }
            query = query.Where(x => x.Usuarios.All(y => y.Id != request.IdUsuario));

            var projecao = query.Select(x => new BolaoListarResponse
            {
                Nome = x.Nome,
                Privado = x.Privado,
                Usuario = x.UsuarioAdm != null ? x.UsuarioAdm.Nome : "Unknown",
                Premio = x.Premios.Where(p => p.Colocacao == 1).Select(p => p.Descricao).ToArray(),
                TemSenha = x.Senha != null && x.Senha.Trim().Length > 0
            });

            return projecao.ToList();
        }

        public BolaoResponse ConstruirResponse(Bolao bolao)
        {
            return new BolaoResponse
            {
                Nome = bolao.Nome,
                Logo = bolao.Logo,
                TokenAcesso = bolao.TokenAcesso,
                Aviso = bolao.Aviso,
                Administrador = bolao.UsuarioAdm.Nome,
                Premios = bolao.Premios.Select(p => new PremioResponse
                {
                    Id = p.Id,
                    Descricao = p.Descricao,
                    Colocacao = p.Colocacao
                }).ToList(),
                Regras = bolao.Regras.Select(r => new BolaoRegraResponse
                {
                    Id = r.Regra.Id,
                    Pontuacao = r.Pontuacao,
                    Descricao = r.Regra.Descricao,
                    Explicacao = r.Regra.Explicacao
                }).ToList()
            };
        }
    }
}

