using AutoMapper;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Models;


namespace BolaoDaCopa.Aplicacao.Boloes.Profiles
{
    public class BoloesProfile : Profile
    {
        public BoloesProfile()
        {
            CreateMap<Regra, RegraResponse>();
            CreateMap<Bolao, BolaoResponse>();
        }
    }
}
