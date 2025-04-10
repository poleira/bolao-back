using AutoMapper;
using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Dto.Cadastros;
using BolaoTeste.Models;


namespace BolaoTeste.Profiles
{
    public class CadastroProfile : Profile
    {
        public CadastroProfile()
        {
            CreateMap<CreateCadastroRequest, Cadastro>();
            CreateMap<Cadastro, CreateCadastroResponse>();
            CreateMap<Cadastro, ChecarUsuarioResponse>();
            CreateMap<ChecarUsuarioRequest, Cadastro>();
        }
    }
}
