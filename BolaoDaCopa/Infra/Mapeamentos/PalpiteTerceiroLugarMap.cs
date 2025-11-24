using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteTerceiroLugarMap : ClassMap<PalpiteTerceiroLugar>
    {
        public PalpiteTerceiroLugarMap()
        {
            Schema("Bolao");
            Table("PalpiteTerceiroLugar");
            Id(x => x.Id).Column("ID");
            References(x => x.BolaoUsuario).Column("IDBolaoUsuario").Not.Nullable();
            References(x => x.Selecao).Column("IDSelecao").Not.Nullable();
            Map(x => x.Posicao).Column("Posicao");
        }
    }
}
