using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{
    public class ModoJogoRegraMap : ClassMap<ModoJogoRegra>
    {
        public ModoJogoRegraMap()
        {
            Schema("bolao");
            Table("modojogoregra");
            Id(x => x.Id).Column("id");
            References(x => x.ModoJogo).Column("idmodojogo").Not.Nullable();
            References(x => x.Regra).Column("idregra").Not.Nullable();
        }
    }
}
