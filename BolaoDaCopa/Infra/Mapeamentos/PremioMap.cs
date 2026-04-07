using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{

    public class PremioMap : ClassMap<Premio>
    {
        public PremioMap()
        {
            Schema("bolao");
            Table("premio");
            Id(x => x.Id).Column("id");
            Map(x => x.Descricao).Column("descricao");
            Map(x => x.Colocacao).Column("colocacao");
            References(x => x.Bolao).Column("idbolao").Not.Nullable().Cascade.None();
        }
    }
}
