using BolaoTeste.Dto.Palpites;
using BolaoTeste.Models;
using ISession = NHibernate.ISession;
using Dapper;
using NHibernate;
using BolaoTeste.Data.Repositorios.Interfaces;
using BolaoTeste.Dto.JogosBr;

namespace BolaoTeste.Data.Repositorios
{
    public class PalpiteRepositorio : IPalpiteRepositorio
    {
        private readonly ISession session;
        private readonly string sqlGrupos = @"UPDATE cadastro
                                        INNER JOIN -G- ON -Idg- = -G-.ID
                                        INNER JOIN oitavas ON IDOITAVAS = OITAVAS.ID
                                        SET -Time1- = @Resp1, -Time2- = @Resp2, -Time3- = @Resp3, -Time4- = @Resp4,
                                        -Time1-Pontos = @Pontos1, -Time2-Pontos = @Pontos2, -Time3-Pontos = @Pontos3, -Time4-Pontos = @Pontos4,
                                        -TIMEX1- = @selecao1, -TIMEX2- = @selecao2
                                        WHERE
                                        usuario = @usuario";
        private readonly string sqlJogosBrGrupos = @"Update cadastro    
                                                   inner join jogosdobr on IDJOGOSDOBR = jogosdobr.id
                                                   set jogo1 = @jogo1, jogo2 = @jogo2, jogo3 = @jogo3
                                                   where usuario = @usuario";
        private readonly string sqlJogosBrMataMata = @"Update cadastro    
                                                   inner join jogosdobr on IDJOGOSDOBR = jogosdobr.id
                                                   set -matamata- = @jogo
                                                   where usuario = @usuario";

        public PalpiteRepositorio(ISession session)
        {
            this.session = session;
        }
        public void EditarJogosBrGrupos(FaseDeGruposJogosBrRequest request)
        {

            DynamicParameters parameters = new();
            parameters.Add("@jogo1", request.JogoBr1);
            parameters.Add("@jogo2", request.JogoBr2);
            parameters.Add("@jogo3", request.JogoBr3);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlJogosBrGrupos, parameters);
        }
        public void EditarJogosBrOitavas(MataMataJogosBrRequest request, string oitavas)
        {

            DynamicParameters parameters = new();
            var sqlReplacement = sqlJogosBrMataMata.Replace("-matamata-", oitavas);
            parameters.Add("@jogo", request.JogoBr);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarJogosBrQuartas(MataMataJogosBrRequest request, string quartas)
        {

            DynamicParameters parameters = new();
            var sqlReplacement = sqlJogosBrMataMata.Replace("-matamata-", quartas);
            parameters.Add("@jogo", request.JogoBr);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarJogosBrSemis(MataMataJogosBrRequest request, string semis)
        {

            DynamicParameters parameters = new();
            var sqlReplacement = sqlJogosBrMataMata.Replace("-matamata-", semis);
            parameters.Add("@jogo", request.JogoBr);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarJogosBrFinais(MataMataJogosBrRequest request, string finais)
        {

            DynamicParameters parameters = new();
            var sqlReplacement = sqlJogosBrMataMata.Replace("-matamata-", finais);
            parameters.Add("@jogo", request.JogoBr);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }

        public void EditarGa(GaEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {

            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Senegal))
                                          .Replace("-Time2-", nameof(request.Holanda))
                                          .Replace("-Time3-", nameof(request.Equador))
                                          .Replace("-Time4-", nameof(request.Qatar))    
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME1")
                                          .Replace("-TIMEX2-", "TIME10");
            parameters.Add("@Resp1", request.Senegal);
            parameters.Add("@Resp2", request.Holanda);
            parameters.Add("@Resp3", request.Equador);
            parameters.Add("@Resp4", request.Qatar);
            parameters.Add("@Pontos1", request.SenegalPontos);
            parameters.Add("@Pontos2", request.HolandaPontos);
            parameters.Add("@Pontos3", request.EquadorPontos);
            parameters.Add("@Pontos4", request.QatarPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }

        public void EditarGb(GbEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {
            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Inglaterra))
                                          .Replace("-Time2-", nameof(request.Iram))
                                          .Replace("-Time3-", nameof(request.USA))
                                          .Replace("-Time4-", nameof(request.PaisDeGales))
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME9")
                                          .Replace("-TIMEX2-", "TIME2");
            parameters.Add("@Resp1", request.Inglaterra);
            parameters.Add("@Resp2", request.Iram);
            parameters.Add("@Resp3", request.USA);
            parameters.Add("@Resp4", request.PaisDeGales);
            parameters.Add("@Pontos1", request.InglaterraPontos);
            parameters.Add("@Pontos2", request.IramPontos);
            parameters.Add("@Pontos3", request.USAPontos);
            parameters.Add("@Pontos4", request.PaisDeGalesPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarGc(GcEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {
            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Argentina))
                                          .Replace("-Time2-", nameof(request.ArabiaSaudita))
                                          .Replace("-Time3-", nameof(request.Mexico))
                                          .Replace("-Time4-", nameof(request.Polonia))
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME3")
                                          .Replace("-TIMEX2-", "TIME12"); 
            parameters.Add("@Resp1", request.Argentina);
            parameters.Add("@Resp2", request.ArabiaSaudita);
            parameters.Add("@Resp3", request.Mexico);
            parameters.Add("@Resp4", request.Polonia);
            parameters.Add("@Pontos1", request.ArgentinaPontos);
            parameters.Add("@Pontos2", request.ArabiaSauditaPontos);
            parameters.Add("@Pontos3", request.MexicoPontos);
            parameters.Add("@Pontos4", request.PoloniaPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarGd(GdEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {
            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Franca))
                                          .Replace("-Time2-", nameof(request.Australia))
                                          .Replace("-Time3-", nameof(request.Dinamarca))
                                          .Replace("-Time4-", nameof(request.Tunisia))
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME11")
                                          .Replace("-TIMEX2-", "TIME4");
            parameters.Add("@Resp1", request.Franca);
            parameters.Add("@Resp2", request.Australia);
            parameters.Add("@Resp3", request.Dinamarca);
            parameters.Add("@Resp4", request.Tunisia);
            parameters.Add("@Pontos1", request.FrancaPontos);
            parameters.Add("@Pontos2", request.AustraliaPontos);
            parameters.Add("@Pontos3", request.DinamarcaPontos);
            parameters.Add("@Pontos4", request.TunisiaPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarGe(GeEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {
            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Espanha))
                                          .Replace("-Time2-", nameof(request.CostaRica))
                                          .Replace("-Time3-", nameof(request.Alemanha))
                                          .Replace("-Time4-", nameof(request.Japao))
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME5")
                                          .Replace("-TIMEX2-", "TIME14");
            parameters.Add("@Resp1", request.Espanha);
            parameters.Add("@Resp2", request.CostaRica);
            parameters.Add("@Resp3", request.Alemanha);
            parameters.Add("@Resp4", request.Japao);
            parameters.Add("@Pontos1", request.EspanhaPontos);
            parameters.Add("@Pontos2", request.CostaRicaPontos);
            parameters.Add("@Pontos3", request.AlemanhaPontos);
            parameters.Add("@Pontos4", request.JapaoPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarGf(GfEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {
            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Belgica))
                                          .Replace("-Time2-", nameof(request.Canada))
                                          .Replace("-Time3-", nameof(request.Marrocos))
                                          .Replace("-Time4-", nameof(request.Croacia))
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME13")
                                          .Replace("-TIMEX2-", "TIME6");
            parameters.Add("@Resp1", request.Belgica);
            parameters.Add("@Resp2", request.Canada);
            parameters.Add("@Resp3", request.Marrocos);
            parameters.Add("@Resp4", request.Croacia);
            parameters.Add("@Pontos1", request.BelgicaPontos);
            parameters.Add("@Pontos2", request.CanadaPontos);
            parameters.Add("@Pontos3", request.MarrocosPontos);
            parameters.Add("@Pontos4", request.CroaciaPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarGg(GgEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {
            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Brasil))
                                          .Replace("-Time2-", nameof(request.Servia))
                                          .Replace("-Time3-", nameof(request.Suica))
                                          .Replace("-Time4-", nameof(request.Camaroes))
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME7")
                                          .Replace("-TIMEX2-", "TIME16");
            parameters.Add("@Resp1", request.Brasil);
            parameters.Add("@Resp2", request.Servia);
            parameters.Add("@Resp3", request.Suica);
            parameters.Add("@Resp4", request.Camaroes);
            parameters.Add("@Pontos1", request.BrasilPontos);
            parameters.Add("@Pontos2", request.ServiaPontos);
            parameters.Add("@Pontos3", request.SuicaPontos);
            parameters.Add("@Pontos4", request.CamaroesPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }
        public void EditarGh(GhEditarRequest request, string Idg, string g, string primeiro, string segundo)
        {
            DynamicParameters parameters = new();
            var sqlReplacement = sqlGrupos.Replace("-Time1-", nameof(request.Portugal))
                                          .Replace("-Time2-", nameof(request.Gana))
                                          .Replace("-Time3-", nameof(request.Uruguai))
                                          .Replace("-Time4-", nameof(request.CoreiaDoSul))
                                          .Replace("-G-", g)
                                          .Replace("-Idg-", Idg)
                                          .Replace("-TIMEX1-", "TIME15")
                                          .Replace("-TIMEX2-", "TIME8");
            parameters.Add("@Resp1", request.Portugal);
            parameters.Add("@Resp2", request.Gana);
            parameters.Add("@Resp3", request.Uruguai);
            parameters.Add("@Resp4", request.CoreiaDoSul);
            parameters.Add("@Pontos1", request.PortugalPontos);
            parameters.Add("@Pontos2", request.GanaPontos);
            parameters.Add("@Pontos3", request.UruguaiPontos);
            parameters.Add("@Pontos4", request.CoreiaDoSulPontos);
            parameters.Add("@selecao1", primeiro);
            parameters.Add("@selecao2", segundo);
            parameters.Add("@usuario", request.Usuario);

            session.Connection.Execute(sqlReplacement, parameters);
        }


    }
}
