using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{
    public class ModoJogoMap : ClassMap<ModoJogo>
    {
        public ModoJogoMap()
        {
            Schema("bolao");
            Table("modojogo");
            Id(x => x.Id).Column("id");
            Map(x => x.NomeModoJogo).Column("nomemodojogo");

            HasMany(x => x.Regras)
                .KeyColumn("idmodojogo")
                .Cascade.All().Not.LazyLoad();
        }
    }
}
