using AutoMapper;
using BolaoDaCopa.Dto.BoloesRegras.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.BolaoRegras.Profiles
{
    public class BoloesRegrasProfile : Profile
    {
        public BoloesRegrasProfile()
        {
            CreateMap<BolaoRegra, BolaoRegraResponse>()
                .ForMember(x => x.Descricao, y => y.MapFrom(z => z.Regra.Descricao))
                .ForMember(x => x.Explicacao, y => y.MapFrom(z => z.Regra.Explicacao));
        }
    }
}
