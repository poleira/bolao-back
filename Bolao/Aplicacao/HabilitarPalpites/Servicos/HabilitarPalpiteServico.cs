using AutoMapper;
using BolaoTeste.Aplicacao.HabilitarPalpites.Servicos.Interfaces;
using BolaoTeste.Data.Repositorios.Interfaces;
using BolaoTeste.Dto.HabilitarPalpites;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Aplicacao.HabilitarPalpites.Servicos
{
    public class HabilitarPalpiteServico : IHabilitarPalpiteServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly IHabilitarPalpiteRepositorio habilitarPalpiteRepositorio;


        public HabilitarPalpiteServico(ISession session, IMapper mapper, IHabilitarPalpiteRepositorio habilitarPalpiteRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.habilitarPalpiteRepositorio = habilitarPalpiteRepositorio;
        }
        public HabilitarPalpiteResponse Recuperar()
        {
            var queryHabilitarPalpite = habilitarPalpiteRepositorio.Query().FirstOrDefault();
            var retorno = mapper.Map<HabilitarPalpiteResponse>(queryHabilitarPalpite);
            return retorno;
        }
    }
}
