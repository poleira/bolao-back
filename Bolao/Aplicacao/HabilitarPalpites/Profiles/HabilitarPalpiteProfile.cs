using AutoMapper;
using BolaoTeste.Dto.HabilitarPalpites;
using BolaoTeste.Models;
namespace BolaoTeste.Aplicacao.HabilitarPalpites.Profiles
{
    public class HabilitarPalpiteProfile : Profile
    {
        public HabilitarPalpiteProfile()
        {
            CreateMap<HabilitarPalpite,HabilitarPalpiteResponse>();
        }   
    }
}
