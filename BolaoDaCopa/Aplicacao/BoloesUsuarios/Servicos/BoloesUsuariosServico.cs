using AutoMapper;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos
{
    public class BoloesUsuariosServico : IBoloesUsuariosServico
    {
        private readonly IMapper mapper;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;

        public BoloesUsuariosServico(IMapper mapper, IUsuariosRepositorio usuariosRepositorio, IBoloesUsuariosRepositorio boloesUsuariosRepositorio)
        {
            this.mapper = mapper;
            this.usuariosRepositorio = usuariosRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
        }

        public BolaoUsuarioResponse Recuperar(int id)
        {
            BolaoUsuario? bolaoUsuario = boloesUsuariosRepositorio.Recuperar(id) ?? throw new Exception("Bolão não encontrado.");

            BolaoUsuarioResponse? response = mapper.Map<BolaoUsuarioResponse>(bolaoUsuario);

            return response;
        }

        public IEnumerable<BolaoUsuarioResponse> ListarBoloesUsuario(string hashUsuario)
        {
            Usuario usuario = usuariosRepositorio.RecuperarPorHash(hashUsuario) ?? throw new Exception("Usuário não encontrado.");

            IQueryable<BolaoUsuario> boloesUsuarios = boloesUsuariosRepositorio.Query().Where(x => x.Usuario.Id == usuario.Id);

            IEnumerable<BolaoUsuarioResponse>? response = mapper.Map<IEnumerable<BolaoUsuarioResponse>>(boloesUsuarios);

            return response;
        }
    }
}
