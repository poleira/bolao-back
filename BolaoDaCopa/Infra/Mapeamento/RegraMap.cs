using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class RegraMap : ClassMap<Regra>
    {
        public RegraMap()
        {
            Schema("Bolao");
            Table("Regra");
            Id(x => x.Id).Column("ID");
            Map(x => x.Descricao).Column("Descricao");
            Map(x => x.Explicacao).Column("Explicacao");
        }
    }
}
