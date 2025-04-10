using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Models;

namespace BolaoTeste.Aplicacao.Palpites.Services
{
    public static class TransformarEmSigla
    {
        public static ListarOitavasResponse TransformaEmSigla(ListarOitavasResponse request)
        {
            var oitavas = request;
                switch (oitavas.Time1)
                {
                case "Qatar":
                    oitavas.Time1Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time1Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time1Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time1Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time1Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time1Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time1Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time1Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time1Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time1Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time1Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time1Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time1Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time1Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time1Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time1Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time1Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time1Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time1Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time1Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time1Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time1Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time1Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time1Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time1Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time1Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time1Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time1Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time1Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time1Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time1Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time1Sigla = "kr";
                    break;
                default: oitavas.Time1 = null;
                    break;
                }

            switch (oitavas.Time2)
            {
                case "Qatar":
                    oitavas.Time2Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time2Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time2Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time2Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time2Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time2Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time2Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time2Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time2Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time2Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time2Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time2Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time2Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time2Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time2Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time2Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time2Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time2Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time2Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time2Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time2Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time2Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time2Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time2Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time2Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time2Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time2Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time2Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time2Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time2Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time2Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time2Sigla = "kr";
                    break;
                default:
                    oitavas.Time2 = null;
                    break;
            }

            switch (oitavas.Time3)
            {
                case "Qatar":
                    oitavas.Time3Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time3Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time3Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time3Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time3Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time3Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time3Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time3Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time3Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time3Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time3Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time3Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time3Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time3Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time3Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time3Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time3Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time3Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time3Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time3Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time3Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time3Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time3Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time3Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time3Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time3Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time3Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time3Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time3Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time3Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time3Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time3Sigla = "kr";
                    break;
                default:
                    oitavas.Time3 = null;
                    break;
            }
            switch (oitavas.Time4)
            {
                case "Qatar":
                    oitavas.Time4Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time4Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time4Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time4Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time4Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time4Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time4Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time4Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time4Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time4Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time4Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time4Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time4Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time4Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time4Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time4Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time4Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time4Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time4Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time4Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time4Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time4Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time4Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time4Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time4Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time4Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time4Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time4Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time4Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time4Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time4Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time4Sigla = "kr";
                    break;
                default:
                    oitavas.Time4 = null;
                    break;
            }
            switch (oitavas.Time5)
            {
                case "Qatar":
                    oitavas.Time5Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time5Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time5Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time5Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time5Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time5Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time5Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time5Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time5Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time5Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time5Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time5Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time5Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time5Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time5Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time5Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time5Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time5Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time5Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time5Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time5Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time5Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time5Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time5Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time5Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time5Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time5Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time5Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time5Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time5Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time5Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time5Sigla = "kr";
                    break;
                default:
                    oitavas.Time5 = null;
                    break;
            }
            switch (oitavas.Time6)
            {
                case "Qatar":
                    oitavas.Time6Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time6Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time6Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time6Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time6Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time6Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time6Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time6Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time6Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time6Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time6Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time6Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time6Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time6Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time6Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time6Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time6Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time6Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time6Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time6Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time6Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time6Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time6Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time6Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time6Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time6Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time6Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time6Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time6Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time6Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time6Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time6Sigla = "kr";
                    break;
                default:
                    oitavas.Time6 = null;
                    break;
            }
            switch (oitavas.Time7)
            {
                case "Qatar":
                    oitavas.Time7Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time7Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time7Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time7Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time7Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time7Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time7Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time7Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time7Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time7Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time7Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time7Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time7Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time7Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time7Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time7Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time7Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time7Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time7Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time7Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time7Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time7Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time7Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time7Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time7Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time7Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time7Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time7Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time7Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time7Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time7Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time7Sigla = "kr";
                    break;
                default:
                    oitavas.Time7 = null;
                    break;
            }
            switch (oitavas.Time8)
            {
                case "Qatar":
                    oitavas.Time8Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time8Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time8Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time8Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time8Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time8Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time8Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time8Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time8Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time8Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time8Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time8Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time8Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time8Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time8Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time8Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time8Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time8Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time8Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time8Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time8Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time8Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time8Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time8Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time8Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time8Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time8Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time8Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time8Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time8Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time8Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time8Sigla = "kr";
                    break;
                default:
                    oitavas.Time8 = null;
                    break;
            }
            switch (oitavas.Time9)
            {
                case "Qatar":
                    oitavas.Time9Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time9Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time9Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time9Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time9Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time9Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time9Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time9Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time9Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time9Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time9Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time9Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time9Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time9Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time9Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time9Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time9Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time9Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time9Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time9Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time9Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time9Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time9Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time9Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time9Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time9Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time9Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time9Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time9Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time9Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time9Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time9Sigla = "kr";
                    break;
                default:
                    oitavas.Time9 = null;
                    break;
            }
            switch (oitavas.Time10)
            {
                case "Qatar":
                    oitavas.Time10Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time10Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time10Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time10Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time10Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time10Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time10Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time10Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time10Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time10Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time10Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time10Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time10Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time10Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time10Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time10Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time10Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time10Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time10Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time10Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time10Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time10Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time10Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time10Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time10Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time10Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time10Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time10Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time10Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time10Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time10Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time10Sigla = "kr";
                    break;
                default:
                    oitavas.Time10 = null;
                    break;
            }
            switch (oitavas.Time11)
            {
                case "Qatar":
                    oitavas.Time11Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time11Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time11Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time11Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time11Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time11Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time11Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time11Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time11Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time11Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time11Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time11Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time11Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time11Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time11Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time11Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time11Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time11Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time11Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time11Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time11Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time11Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time11Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time11Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time11Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time11Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time11Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time11Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time11Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time11Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time11Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time11Sigla = "kr";
                    break;
                default:
                    oitavas.Time11 = null;
                    break;
            }
            switch (oitavas.Time12)
            {
                case "Qatar":
                    oitavas.Time12Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time12Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time12Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time12Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time12Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time12Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time12Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time12Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time12Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time12Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time12Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time12Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time12Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time12Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time12Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time12Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time12Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time12Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time12Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time12Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time12Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time12Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time12Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time12Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time12Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time12Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time12Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time12Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time12Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time12Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time12Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time12Sigla = "kr";
                    break;
                default:
                    oitavas.Time12 = null;
                    break;
            }
            switch (oitavas.Time13)
            {
                case "Qatar":
                    oitavas.Time13Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time13Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time13Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time13Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time13Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time13Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time13Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time13Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time13Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time13Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time13Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time13Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time13Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time13Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time13Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time13Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time13Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time13Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time13Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time13Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time13Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time13Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time13Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time13Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time13Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time13Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time13Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time13Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time13Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time13Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time13Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time13Sigla = "kr";
                    break;
                default:
                    oitavas.Time13 = null;
                    break;
            }
            switch (oitavas.Time14)
            {
                case "Qatar":
                    oitavas.Time14Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time14Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time14Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time14Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time14Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time14Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time14Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time14Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time14Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time14Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time14Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time14Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time14Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time14Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time14Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time14Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time14Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time14Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time14Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time14Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time14Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time14Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time14Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time14Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time14Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time14Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time14Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time14Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time14Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time14Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time14Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time14Sigla = "kr";
                    break;
                default:
                    oitavas.Time14 = null;
                    break;
            }
            switch (oitavas.Time15)
            {
                case "Qatar":
                    oitavas.Time15Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time15Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time15Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time15Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time15Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time15Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time15Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time15Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time15Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time15Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time15Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time15Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time15Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time15Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time15Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time15Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time15Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time15Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time15Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time15Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time15Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time15Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time15Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time15Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time15Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time15Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time15Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time15Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time15Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time15Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time15Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time15Sigla = "kr";
                    break;
                default:
                    oitavas.Time15 = null;
                    break;
            }
            switch (oitavas.Time16)
            {
                case "Qatar":
                    oitavas.Time16Sigla = "QAT";
                    break;
                case "Equador":
                    oitavas.Time16Sigla = "ECU";
                    break;
                case "Senegal":
                    oitavas.Time16Sigla = "SEN";
                    break;
                case "Holanda":
                    oitavas.Time16Sigla = "Netherlands";
                    break;
                case "Inglaterra":
                    oitavas.Time16Sigla = "england";
                    break;
                case "Iram":
                    oitavas.Time16Sigla = "iran";
                    break;
                case "USA":
                    oitavas.Time16Sigla = "USA";
                    break;
                case "PaisDeGales":
                    oitavas.Time16Sigla = "Wales";
                    break;
                case "Argentina":
                    oitavas.Time16Sigla = "arg";
                    break;
                case "ArabiaSaudita":
                    oitavas.Time16Sigla = "sau";
                    break;
                case "Mexico":
                    oitavas.Time16Sigla = "mex";
                    break;
                case "Polonia":
                    oitavas.Time16Sigla = "Pol";
                    break;
                case "Franca":
                    oitavas.Time16Sigla = "FRA";
                    break;
                case "Australia":
                    oitavas.Time16Sigla = "Aus";
                    break;
                case "Dinamarca":
                    oitavas.Time16Sigla = "Denmark";
                    break;
                case "Tunisia":
                    oitavas.Time16Sigla = "Tun";
                    break;
                case "Espanha":
                    oitavas.Time16Sigla = "spain";
                    break;
                case "CostaRica":
                    oitavas.Time16Sigla = "cr";
                    break;
                case "Alemanha":
                    oitavas.Time16Sigla = "Germany";
                    break;
                case "Japao":
                    oitavas.Time16Sigla = "japan";
                    break;
                case "Belgica":
                    oitavas.Time16Sigla = "bel";
                    break;
                case "Canada":
                    oitavas.Time16Sigla = "Can";
                    break;
                case "Marrocos":
                    oitavas.Time16Sigla = "morocco";
                    break;
                case "Croacia":
                    oitavas.Time16Sigla = "Croatia";
                    break;
                case "Brasil":
                    oitavas.Time16Sigla = "bra";
                    break;
                case "Servia":
                    oitavas.Time16Sigla = "serbia";
                    break;
                case "Suica":
                    oitavas.Time16Sigla = "Switzerland";
                    break;
                case "Camaroes":
                    oitavas.Time16Sigla = "cameroon";
                    break;
                case "Portugal":
                    oitavas.Time16Sigla = "portugal";
                    break;
                case "Gana":
                    oitavas.Time16Sigla = "ghana";
                    break;
                case "Uruguai":
                    oitavas.Time16Sigla = "Uruguay";
                    break;
                case "CoreiaDoSul":
                    oitavas.Time16Sigla = "kr";
                    break;
                default:
                    oitavas.Time16 = null;
                    break;
            }

            return oitavas;

        }
    }
}
