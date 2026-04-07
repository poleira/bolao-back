using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class CampeonatoMap : ClassMap<BolaoRegra>
    {
        public CampeonatoMap()
        {
            Schema("bolao");
            Table("bolaoregra");
            Id(x => x.Id).Column("id");
            Map(x => x.Pontuacao).Column("pontuacao");
            References(x => x.Bolao).Column("idbolao").Not.Nullable();
            References(x => x.Regra).Column("idregra").Not.Nullable();
        }
    }
}
