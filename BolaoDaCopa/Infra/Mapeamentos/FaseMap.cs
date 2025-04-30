using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class FaseMap : ClassMap<Fase>
    {
        public FaseMap()
        {
            Schema("Bolao");
            Table("Fase");
            Id(x => x.Id).Column("ID");
            Map(x => x.Nome).Column("Nome");
            HasMany(x => x.Selecoes)
                .KeyColumn("IDFase")
                .Cascade.All();
        }
    }
}
