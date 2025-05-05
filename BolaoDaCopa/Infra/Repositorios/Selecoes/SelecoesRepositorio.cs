using ISession = NHibernate.ISession;
using Dapper;
using NHibernate;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.Selecoes
{
    public class SelecoesRepositorio : ISelecoesRepositorio
    {
        private readonly ISession session;
        //private readonly string sqlGrupos = @"UPDATE cadastro
        //                                INNER JOIN -G- ON -Idg- = -G-.ID
        //                                INNER JOIN oitavas ON IDOITAVAS = OITAVAS.ID
        //                                SET -Time1- = @Resp1, -Time2- = @Resp2, -Time3- = @Resp3, -Time4- = @Resp4,
        //                                -Time1-Pontos = @Pontos1, -Time2-Pontos = @Pontos2, -Time3-Pontos = @Pontos3, -Time4-Pontos = @Pontos4,
        //                                -TIMEX1- = @selecao1, -TIMEX2- = @selecao2
        //                                WHERE
        //                                usuario = @usuario";

        public SelecoesRepositorio(ISession session)
        {
            this.session = session;
        }

        public IQueryable<Selecao> QuerySelecao()
        {
            session.Clear();
            return session.Query<Selecao>();
        }


        public Selecao Recuperar(int id)
        {
            return session.Get<Selecao>(id);
        }

        public IQueryable<Fase> RecuperarQueryFasePorId(int idFase)
        {
            return session.Query<Fase>()
                .Where(x => x.Id == idFase);
        }
        public async Task<Grupo> RecuperarGrupo(int id)
        {
            return await session.GetAsync<Grupo>(id);
        }

        //public void EditarGa(GaEditarRequest request, string Idg, string g, string primeiro, string segundo)
        //{

        //    DynamicParameters parameters = new();
        //    var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Senegal))
        //                                  .Replace("-Time2-", nameof(request.Holanda))
        //                                  .Replace("-Time3-", nameof(request.Equador))
        //                                  .Replace("-Time4-", nameof(request.Qatar))    
        //                                  .Replace("-G-", g)
        //                                  .Replace("-Idg-", Idg)
        //                                  .Replace("-TIMEX1-", "TIME1")
        //                                  .Replace("-TIMEX2-", "TIME10");
        //    parameters.Add("@Resp1", request.Senegal);
        //    parameters.Add("@Resp2", request.Holanda);
        //    parameters.Add("@Resp3", request.Equador);
        //    parameters.Add("@Resp4", request.Qatar);
        //    parameters.Add("@Pontos1", request.SenegalPontos);
        //    parameters.Add("@Pontos2", request.HolandaPontos);
        //    parameters.Add("@Pontos3", request.EquadorPontos);
        //    parameters.Add("@Pontos4", request.QatarPontos);
        //    parameters.Add("@selecao1", primeiro);
        //    parameters.Add("@selecao2", segundo);
        //    parameters.Add("@usuario", request.Usuario);

        //    session.Connection.Execute(sqlReplacement, parameters);
        //}



    }
}
