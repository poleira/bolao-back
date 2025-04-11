using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class SelecaoMap : ClassMap<Selecao>
    {
        public SelecaoMap()
        {
            Schema("Bolao");
            Table("Selecao");
            Id(x => x.Id).Column("ID");
            Map(x => x.Nome).Column("Nome");
            Map(x => x.Logo).Column("Logo");
            Map(x => x.Abreviacao).Column("Abreviacao");
        }
    }
}
