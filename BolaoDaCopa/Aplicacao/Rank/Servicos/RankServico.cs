using AutoMapper;
using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoTeste.Dto.Rank;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.Rank.Servicos
{
    public class RankServico : IRankServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly IBoloesRepositorio cadastroRepositorio;
        private readonly IUsuariosRepositorio campeonatoRepositorio;

        public RankServico(ISession session, IMapper mapper, IBoloesRepositorio cadastroRepositorio, IUsuariosRepositorio campeonatoRepositorio)
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
