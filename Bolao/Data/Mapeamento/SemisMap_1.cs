using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Semis_1Map : ClassMap<Semis_1>
    {
        public Semis_1Map()
        {
            Schema("teste");
            Table("semis_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.time1).Column("TIME1");
            Map(x => x.time2).Column("TIME2");
            Map(x => x.time3).Column("TIME3");
            Map(x => x.time4).Column("TIME4");
        }
    }
}
