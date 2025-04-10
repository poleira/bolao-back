using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class HabilitarPalpiteMap : ClassMap<HabilitarPalpite>
    {
        public HabilitarPalpiteMap()
        {
            Schema("teste");
            Table("habilitarpalpite");
            Id(x => x.Id).Column("ID");
            Map(x => x.Geral).Column("GERAL");
            Map(x => x.Oitavas).Column("OITAVAS");
            Map(x => x.Quartas).Column("QUARTAS");
            Map(x => x.Semis).Column("SEMIS");
            Map(x => x.Finais).Column("FINAIS");

        }
    }
}
