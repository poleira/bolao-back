using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class FaseMap : ClassMap<Fase>
    {
        public FaseMap()
        {
            Schema("bolao");
            Table("fase");
            Id(x => x.Id).Column("id");
            Map(x => x.Nome).Column("nome");
            HasMany(x => x.Selecoes)
                .KeyColumn("idfase")
                .Cascade.All();
        }
    }
}
