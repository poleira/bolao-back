using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class MelhorTerceiroLugarMap : ClassMap<MelhorTerceiroLugar>
    {
        public MelhorTerceiroLugarMap()
        {
            Schema("Bolao");
            Table("MelhorTerceiroLugar");
            Id(x => x.Id).Column("ID");
            References(x => x.Selecao).Column("IDSelecao").Not.Nullable();
            Map(x => x.Posicao).Column("Posicao");
        }
    }
}
