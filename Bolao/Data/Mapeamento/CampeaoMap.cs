using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class CampeaoMap : ClassMap<Campeao>
    {
        public CampeaoMap()
        {
            Schema("teste");
            Table("campeao");
            Id(x => x.Id).Column("ID");
            Map(x => x.Time).Column("TIME");
        }
    }
}
