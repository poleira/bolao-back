using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Finais_1Map : ClassMap<Finais_1>
    {
        public Finais_1Map()
        {
            Schema("teste");
            Table("finais_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.Time1).Column("TIME1");
            Map(x => x.Time2).Column("TIME2");
        }
    }
}
