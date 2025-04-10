using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Jogos_BR_1Map : ClassMap<Jogos_BR_1>
    {
        public Jogos_BR_1Map()
        {
            Schema("teste");
            Table("jogosdobr_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.jogo1).Column("JOGO1");
            Map(x => x.jogo2).Column("jOGO2");
            Map(x => x.jogo3).Column("jOGO3");
            Map(x => x.oitavas).Column("OITAVAS");
            Map(x => x.quartas).Column("QUARTAS");
            Map(x => x.semis).Column("SEMIS");
            Map(x => x.final).Column("FINAL");
        }
    }
}
