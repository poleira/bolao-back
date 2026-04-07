using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{
    public class RegraMap : ClassMap<Regra>
    {
        public RegraMap()
        {
            Schema("bolao");
            Table("regra");
            Id(x => x.Id).Column("id");
            Map(x => x.Descricao).Column("descricao");
            Map(x => x.Explicacao).Column("explicacao");
        }
    }
}
