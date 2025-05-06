

using AutoMapper;
using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Regras
{
    public class RegrasProfile : Profile
    {
        public RegrasProfile()
        {
            CreateMap<Regra, RegraResponse>();
        }
    }
}
