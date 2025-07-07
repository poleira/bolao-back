using AutoMapper;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Notificacoes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;
using BolaoDaCopa.Infra.Repositorios.Boloes;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Models.Enums;
using BolaoDaCopa.Services;

namespace BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos
{
    public class BoloesUsuariosServico : IBoloesUsuariosServico
    {
        private readonly IMapper mapper;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;
        private readonly IBoloesRepositorio boloesRepositorio;
        private readonly INotificacoesServico notificacoesServico;
        private readonly IUnitOfWork unitOfWork;

        public BoloesUsuariosServico(IMapper mapper, IUsuariosRepositorio usuariosRepositorio, IBoloesUsuariosRepositorio boloesUsuariosRepositorio, IBoloesRepositorio boloesRepositorio, INotificacoesServico notificacoesServico, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.usuariosRepositorio = usuariosRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
            this.boloesRepositorio = boloesRepositorio;
            this.notificacoesServico = notificacoesServico;
            this.unitOfWork = unitOfWork;
        }

        public BolaoUsuarioResponse Recuperar(int id)
        {
            BolaoUsuario? bolaoUsuario = boloesUsuariosRepositorio.Recuperar(id) ?? throw new Exception("Bolão não encontrado.");

            BolaoUsuarioResponse? response = mapper.Map<BolaoUsuarioResponse>(bolaoUsuario);

            return response;
        }

        public IEnumerable<BolaoUsuarioResponse> ListarBoloesPorUsuario(int idUsuario)
        {
            Usuario usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");

            IQueryable<BolaoUsuario> boloesUsuarios = boloesUsuariosRepositorio.Query().Where(x => x.Usuario.Id == usuario.Id);

            IEnumerable<BolaoUsuarioResponse>? response = mapper.Map<IEnumerable<BolaoUsuarioResponse>>(boloesUsuarios);

            return response;
        }

        public void AssociarUsuarioBolaoViaHub(AssociarBolaoUsuarioViaHubRequest request)
        {
            try
            {
                unitOfWork.BeginTransaction();
                Bolao bolao = boloesRepositorio.Query().FirstOrDefault(x => x.Nome == request.NomeBolao) ?? throw new Exception("Bolão não encontrado.");
                Usuario usuario = usuariosRepositorio.Recuperar(request.IdUsuario.Value) ?? throw new Exception("Usuário não encontrado.");

                bool usuarioJaAssociado = boloesUsuariosRepositorio.Query()
                    .Any(x => x.Bolao.Nome == bolao.Nome && x.Usuario.Id == usuario.Id);

                if (usuarioJaAssociado)
                {
                    unitOfWork.Rollback();
                    throw new Exception("Usuário já está associado a este bolão.");
                }
                if (!bolao.Privado)
                {
                    if (request.Senha == bolao.Senha)
                    {
                        var comando = new BolaoUsuarioComando(usuario.Id, bolao.Id);
                        BolaoUsuario bolaoUsuario = new(usuario, bolao);
                        boloesUsuariosRepositorio.Inserir(bolaoUsuario);
                    }
                    else
                    {
                        unitOfWork.Rollback();
                        throw new Exception("Senha inválida.");
                    }
                }
                else
                {
                    string mensagem = $"O usuario {usuario.Nome} quer fazer parte do seu bolao {bolao.Nome}. Clique para interagir.";
                    Notificacao notificacao = new(mensagem, TipoMensagemEnum.Solicitacao, false, bolao.UsuarioAdm, usuario, bolao.TokenAcesso);
                    notificacoesServico.CriarNotificacao(notificacao);
                }
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
