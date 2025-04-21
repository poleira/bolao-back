using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;

namespace BolaoDaCopa.Aplicacao.Usuarios.Servicos
{
    public class UsuariosServico : IUsuariosServico
    {
        private readonly IUsuariosRepositorio usuariosRepositorio;
        public UsuariosServico(IUsuariosRepositorio usuariosRepositorio)
        {
            this.usuariosRepositorio = usuariosRepositorio;
        }
    }
}
