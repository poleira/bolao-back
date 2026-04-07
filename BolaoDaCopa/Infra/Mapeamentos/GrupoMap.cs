using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class GrupoMap : ClassMap<Grupo>
    {
        public GrupoMap()
        {
            Schema("bolao");
            Table("grupo");
            Id(x => x.Id).Column("id");
            Map(x => x.Nome).Column("nome");
        }
    }
}
