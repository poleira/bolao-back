using AutoMapper;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Models;


namespace BolaoDaCopa.Aplicacao.Boloes.Profiles
{
    public class BoloesProfile : Profile
    {
        public BoloesProfile()
        {
            CreateMap<Bolao, BolaoResponse>()
                .ForMember(x => x.Administrador, y => y.MapFrom(z => z.UsuarioAdm.Nome));
        }
    }
}
