using AutoMapper;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;
using BolaoDaCopa.Infra.Repositorios.Boloes;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;

namespace BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos
{
    public class BoloesUsuariosServico : IBoloesUsuariosServico
    {
        private readonly IMapper mapper;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;
        private readonly IBoloesRepositorio boloesRepositorio;

        public BoloesUsuariosServico(IMapper mapper, IUsuariosRepositorio usuariosRepositorio, IBoloesUsuariosRepositorio boloesUsuariosRepositorio, IBoloesRepositorio boloesRepositorio)
        {
            this.mapper = mapper;
            this.usuariosRepositorio = usuariosRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
            this.boloesRepositorio = boloesRepositorio;
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
                Bolao bolao = boloesRepositorio.Query().FirstOrDefault(x => x.Nome == request.NomeBolao) ?? throw new Exception("Bolão não encontrado.");
                Usuario usuario = usuariosRepositorio.Recuperar(request.IdUsuario.Value) ?? throw new Exception("Usuário não encontrado.");

                bool usuarioJaAssociado = boloesUsuariosRepositorio.Query()
                    .Any(x => x.Bolao.Nome == bolao.Nome && x.Usuario.Id == usuario.Id);

                if (usuarioJaAssociado)
                {
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
                        throw new Exception("Senha inválida.");
                    }
                }
                else
                {
                    //logica pra enviar solicitação de associação pro adm
                }
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
