using AutoMapper;
using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
using BolaoTeste.Data.Interfaces;
using BolaoTeste.Data.Repositorios.Interfaces;
using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Rank;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.Rank.Servicos
{
    public class RankServico : IRankServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly ICadastroRepositorio cadastroRepositorio;
        private readonly ICampeonatoRepositorio campeonatoRepositorio;

        public RankServico(ISession session, IMapper mapper, ICadastroRepositorio cadastroRepositorio, ICampeonatoRepositorio campeonatoRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.cadastroRepositorio = cadastroRepositorio;
            this.campeonatoRepositorio = campeonatoRepositorio;
        }

        public IList<RankResponse> ListarRank()
        {
            try
            {

                //var retorno = RankService.ListarRank(cadastroLista, campeonato);
                
                return null;
            }
            catch
            {
                return null;
            }
        }


    }
}
