using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{
    public class ModoJogoRegraMap : ClassMap<ModoJogoRegra>
    {
        public ModoJogoRegraMap()
        {
            Schema("Bolao");
            Table("ModoJogoRegra");
            Id(x => x.Id).Column("ID");
            References(x => x.ModoJogo).Column("IDModoJogo").Not.Nullable();
            References(x => x.Regra).Column("IDRegra").Not.Nullable();
        }
    }
}
