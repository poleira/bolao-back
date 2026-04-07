using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class MelhorTerceiroLugarMap : ClassMap<MelhorTerceiroLugar>
    {
        public MelhorTerceiroLugarMap()
        {
            Schema("bolao");
            Table("melhorterceirolugar");
            Id(x => x.Id).Column("id");
            References(x => x.Selecao).Column("idselecao").Not.Nullable();
            Map(x => x.Posicao).Column("posicao");
        }
    }
}
