using AutoMapper;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Premios.Profiles
{
    public class PremiosProfile : Profile
    {
        public PremiosProfile()
        {
            CreateMap<Premio, PremioResponse>();
        }
    }
}
