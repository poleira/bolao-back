using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class FinaisMap : ClassMap<Finais>
    {
        public FinaisMap()
        {
            Schema("teste");
            Table("finais");
            Id(x => x.Id).Column("ID");
            Map(x => x.Time1).Column("TIME1");
            Map(x => x.Time2).Column("TIME2");
        }
    }
}
