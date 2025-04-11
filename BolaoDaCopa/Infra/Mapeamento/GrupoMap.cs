using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class GrupoMap : ClassMap<Grupo>
    {
        public GrupoMap()
        {
            Schema("Bolao");
            Table("Grupo");
            Id(x => x.Id).Column("ID");
            Map(x => x.Nome).Column("Nome");
            HasMany(x => x.Selecoes).KeyColumn("IDGrupo")
                .Cascade.All()
                .Inverse();
        }
    }
}
