using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{
    public class ModosJogosMap : ClassMap<ModoJogo>
    {
        public ModosJogosMap()
        {
            Schema("Bolao");
            Table("ModoJogo");
            Id(x => x.Id).Column("ID");
            Map(x => x.NomeModoJogo).Column("NomeModoJogo");

            HasMany(x => x.Regras)
                .KeyColumn("IDModoJogo")
                .Cascade.All().Not.LazyLoad();
        }
    }
}
