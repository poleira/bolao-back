using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class CampeaoMap_1 : ClassMap<Campeao_1>
    {
        public CampeaoMap_1()
        {
            Schema("teste");
            Table("campeao_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.Time).Column("TIME");
        }
    }
}
