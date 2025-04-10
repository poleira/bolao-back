using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Rank;
using BolaoTeste.Models;

namespace BolaoTeste.Aplicacao.Rank.Servicos.Services
{
    public static class RankService
    {
        public static IList<RankResponse> ListarRank(IList<ListarPalpiteResponse> listaPalpiteUsuario, ListarPalpiteResponse resultado)
        {
            var listaOitavasResposta = new List<string>();
            var listaQuartasResposta = new List<string>();
            var listaSemisResposta = new List<string>();
            var listaFinaisResposta = new List<string>();
            var response = new List<RankResponse>();
            

            listaOitavasResposta.Add(resultado.OitavasTime1);
            listaOitavasResposta.Add(resultado.OitavasTime2);
            listaOitavasResposta.Add(resultado.OitavasTime3);
            listaOitavasResposta.Add(resultado.OitavasTime4);
            listaOitavasResposta.Add(resultado.OitavasTime5);
            listaOitavasResposta.Add(resultado.OitavasTime6);
            listaOitavasResposta.Add(resultado.OitavasTime7);
            listaOitavasResposta.Add(resultado.OitavasTime8);
            listaOitavasResposta.Add(resultado.OitavasTime9);
            listaOitavasResposta.Add(resultado.OitavasTime10);
            listaOitavasResposta.Add(resultado.OitavasTime11);
            listaOitavasResposta.Add(resultado.OitavasTime12);
            listaOitavasResposta.Add(resultado.OitavasTime13);
            listaOitavasResposta.Add(resultado.OitavasTime14);
            listaOitavasResposta.Add(resultado.OitavasTime15);
            listaOitavasResposta.Add(resultado.OitavasTime16);

            listaQuartasResposta.Add(resultado.QuartasTime1);
            listaQuartasResposta.Add(resultado.QuartasTime2);
            listaQuartasResposta.Add(resultado.QuartasTime3);
            listaQuartasResposta.Add(resultado.QuartasTime4);
            listaQuartasResposta.Add(resultado.QuartasTime5);
            listaQuartasResposta.Add(resultado.QuartasTime6);
            listaQuartasResposta.Add(resultado.QuartasTime7);
            listaQuartasResposta.Add(resultado.QuartasTime8);

            listaSemisResposta.Add(resultado.SemisTime1);
            listaSemisResposta.Add(resultado.SemisTime2);
            listaSemisResposta.Add(resultado.SemisTime3);
            listaSemisResposta.Add(resultado.SemisTime4);

            listaFinaisResposta.Add(resultado.FinaisTime1);
            listaFinaisResposta.Add(resultado.FinaisTime2);


            foreach (var item in listaPalpiteUsuario)
            {


                int pontos = 0;
                var listaOitavas = new List<string>();
                var listaQuartas = new List<string>();
                var listaSemis = new List<string>();
                var listaFinais = new List<string>();


                listaOitavas.Add(item.OitavasTime1);
                listaOitavas.Add(item.OitavasTime2);
                listaOitavas.Add(item.OitavasTime3);
                listaOitavas.Add(item.OitavasTime4);
                listaOitavas.Add(item.OitavasTime5);
                listaOitavas.Add(item.OitavasTime6);
                listaOitavas.Add(item.OitavasTime7);
                listaOitavas.Add(item.OitavasTime8);
                listaOitavas.Add(item.OitavasTime9);
                listaOitavas.Add(item.OitavasTime10);
                listaOitavas.Add(item.OitavasTime11);
                listaOitavas.Add(item.OitavasTime12);
                listaOitavas.Add(item.OitavasTime13);
                listaOitavas.Add(item.OitavasTime14);
                listaOitavas.Add(item.OitavasTime15);
                listaOitavas.Add(item.OitavasTime16);

                listaQuartas.Add(item.QuartasTime1);
                listaQuartas.Add(item.QuartasTime2);
                listaQuartas.Add(item.QuartasTime3);
                listaQuartas.Add(item.QuartasTime4);
                listaQuartas.Add(item.QuartasTime5);
                listaQuartas.Add(item.QuartasTime6);
                listaQuartas.Add(item.QuartasTime7);
                listaQuartas.Add(item.QuartasTime8);

                listaSemis.Add(item.SemisTime1);
                listaSemis.Add(item.SemisTime2);
                listaSemis.Add(item.SemisTime3);
                listaSemis.Add(item.SemisTime4);

                listaFinais.Add(item.FinaisTime1);
                listaFinais.Add(item.FinaisTime2);

                //paisesPos

                if (item.Qatar == resultado.Qatar)
                {
                    pontos = pontos + 2;
                }
                if (item.Equador == resultado.Equador)
                {
                    pontos = pontos + 2;
                }
                if (item.Senegal == resultado.Senegal)
                {
                    pontos = pontos + 2;
                }
                if (item.Holanda == resultado.Holanda)
                {
                    pontos = pontos + 2;
                }
                if (item.Inglaterra == resultado.Inglaterra)
                {
                    pontos = pontos + 2;
                }
                if (item.Iram == resultado.Iram)
                {
                    pontos = pontos + 2;
                }
                if (item.USA == resultado.USA)
                {
                    pontos = pontos + 2;
                }
                if (item.PaisDeGales == resultado.PaisDeGales)
                {
                    pontos = pontos + 2;
                }
                if (item.Argentina == resultado.Argentina)
                {
                    pontos = pontos + 2;
                }
                if (item.ArabiaSaudita == resultado.ArabiaSaudita)
                {
                    pontos = pontos + 2;
                }
                if (item.Mexico == resultado.Mexico)
                {
                    pontos = pontos + 2;
                }
                if (item.Polonia == resultado.Polonia)
                {
                    pontos = pontos + 2;
                }
                if (item.Franca == resultado.Franca)
                {
                    pontos = pontos + 2;
                }
                if (item.Australia == resultado.Australia)
                {
                    pontos = pontos + 2;
                }
                if (item.Dinamarca == resultado.Dinamarca)
                {
                    pontos = pontos + 2;
                }
                if (item.Tunisia == resultado.Tunisia)
                {
                    pontos = pontos + 2;
                }
                if (item.Espanha == resultado.Espanha)
                {
                    pontos = pontos + 2;
                }
                if (item.CostaRica == resultado.CostaRica)
                {
                    pontos = pontos + 2;
                }
                if (item.Alemanha == resultado.Alemanha)
                {
                    pontos = pontos + 2;
                }
                if (item.Japao == resultado.Japao)
                {
                    pontos = pontos + 2;
                }
                if (item.Belgica == resultado.Belgica)
                {
                    pontos = pontos + 2;
                }
                if (item.Canada == resultado.Canada)
                {
                    pontos = pontos + 2;
                }
                if (item.Marrocos == resultado.Marrocos)
                {
                    pontos = pontos + 2;
                }
                if (item.Croacia == resultado.Croacia)
                {
                    pontos = pontos + 2;
                }
                if (item.Brasil == resultado.Brasil)
                {
                    pontos = pontos + 2;
                }
                if (item.Servia == resultado.Servia)
                {
                    pontos = pontos + 2;
                }
                if (item.Suica == resultado.Suica)
                {
                    pontos = pontos + 2;
                }
                if (item.Camaroes == resultado.Camaroes)
                {
                    pontos = pontos + 2;
                }
                if (item.Portugal == resultado.Portugal)
                {
                    pontos = pontos + 2;
                }
                if (item.Gana == resultado.Gana)
                {
                    pontos = pontos + 2;
                }
                if (item.Uruguai == resultado.Uruguai)
                {
                    pontos = pontos + 2;
                }
                if (item.CoreiaDoSul == resultado.CoreiaDoSul)
                {
                    pontos = pontos + 2;
                }

                //---------- pontos

                if (item.QatarPontos == resultado.QatarPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.EquadorPontos == resultado.EquadorPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.SenegalPontos == resultado.SenegalPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.HolandaPontos == resultado.HolandaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.InglaterraPontos == resultado.InglaterraPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.IramPontos == resultado.IramPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.USAPontos == resultado.USAPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.PaisDeGalesPontos == resultado.PaisDeGalesPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.ArgentinaPontos == resultado.ArgentinaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.ArabiaSauditaPontos == resultado.ArabiaSauditaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.MexicoPontos == resultado.MexicoPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.PoloniaPontos == resultado.PoloniaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.FrancaPontos == resultado.FrancaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.AustraliaPontos == resultado.AustraliaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.DinamarcaPontos == resultado.DinamarcaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.TunisiaPontos == resultado.TunisiaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.EspanhaPontos == resultado.EspanhaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.CostaRicaPontos == resultado.CostaRicaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.AlemanhaPontos == resultado.AlemanhaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.JapaoPontos == resultado.JapaoPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.BelgicaPontos == resultado.BelgicaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.CanadaPontos == resultado.CanadaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.MarrocosPontos == resultado.MarrocosPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.CroaciaPontos == resultado.CroaciaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.BrasilPontos == resultado.BrasilPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.ServiaPontos == resultado.ServiaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.SuicaPontos == resultado.SuicaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.CamaroesPontos == resultado.CamaroesPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.PortugalPontos == resultado.PortugalPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.GanaPontos == resultado.GanaPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.UruguaiPontos == resultado.UruguaiPontos)
                {
                    pontos = pontos + 2;
                }
                if (item.CoreiaDoSulPontos == resultado.CoreiaDoSulPontos)
                {
                    pontos = pontos + 2;
                }

                //-----------JogosBr

                if (item.Jogo1 == resultado.Jogo1)
                {
                    pontos = pontos + 5;
                }
                if (item.Jogo2 == resultado.Jogo2)
                {
                    pontos = pontos + 5;
                }
                if (item.Jogo3 == resultado.Jogo3)
                {
                    pontos = pontos + 5;
                }
                if (item.Oitavas == resultado.Oitavas)
                {
                    pontos = pontos + 5;
                }
                if (item.Quartas == resultado.Quartas)
                {
                    pontos = pontos + 5;
                }
                if (item.Semis == resultado.Semis)
                {
                    pontos = pontos + 5;
                }
                if (item.Final == resultado.Final)
                {
                    pontos = pontos + 5;
                }

                if (item.Campeao == resultado.Campeao)
                {
                    pontos = pontos + 10;
                }


                foreach (var oitavas in listaOitavas)
                {
                    foreach (var oitavasResposta in listaOitavasResposta)
                    {
                        if (oitavas == oitavasResposta)
                            pontos = pontos + 2;
                    }
                }

                foreach (var quartas in listaQuartas)
                {
                    foreach (var quartasResposta in listaQuartasResposta)
                    {
                        if (quartas == quartasResposta)
                            pontos = pontos + 4;
                    }
                }

                foreach (var semis in listaSemis)
                {
                    foreach (var semisResposta in listaSemisResposta)
                    {
                        if (semis == semisResposta)
                            pontos = pontos + 6;
                    }
                }

                foreach (var finais in listaFinais)
                {
                    foreach (var finaisResposta in listaFinaisResposta)
                    {
                        if (finais == finaisResposta)
                            pontos = pontos + 8;
                    }
                }

                response.Add(new RankResponse (item.Usuario, pontos));

            }

            List<RankResponse> resposta = (from a in response
                           orderby a.Pontuacao
                           select a).ToList();

            resposta.Reverse();
            return resposta;
        }
    }
}
