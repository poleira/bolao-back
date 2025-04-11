using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class CampeonatoMap : ClassMap<BolaoRegra>
    {
        public CampeonatoMap()
        {
            Schema("Bolao");
            Table("BolaoRegra");
            Id(x => x.Id).Column("ID");
            Map(x => x.Pontuacao).Column("Pontuacao");
            References(x => x.Bolao).Column("IDBolao").Not.Nullable();
            References(x => x.Regra).Column("IDRegra").Not.Nullable();
        }
    }
}
