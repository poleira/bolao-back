using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Quartas_1Map : ClassMap<Quartas_1>
    {
        public Quartas_1Map()
        {
            Schema("teste");
            Table("quartas_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.time1).Column("TIME1");
            Map(x => x.time2).Column("TIME2");
            Map(x => x.time3).Column("TIME3");
            Map(x => x.time4).Column("TIME4");
            Map(x => x.time5).Column("TIME5");
            Map(x => x.time6).Column("TIME6");
            Map(x => x.time7).Column("TIME7");
            Map(x => x.time8).Column("TIME8");
        }
    }
}
