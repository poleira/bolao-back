using AutoMapper;
using BolaoDaCopa.Dto.Usuarios;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Usuarios.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            CreateMap<Usuario, UsuarioResponse>();
        }
    }
}
