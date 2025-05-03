using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{

    public class PremioMap : ClassMap<Premio>
    {
        public PremioMap()
        {
            Schema("Bolao");
            Table("Premio");
            Id(x => x.Id).Column("ID");
            Map(x => x.Descricao).Column("Descricao");
            Map(x => x.Colocacao).Column("Explicacao");
            References(x => x.Bolao).Column("IDBolao").Not.Nullable().Cascade.None();
        }
    }
}
