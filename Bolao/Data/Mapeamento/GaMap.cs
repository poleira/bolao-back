using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class GaMap : ClassMap<Ga>
    {
        public GaMap()
        {
            Schema("teste");
            Table("ga");
            Id(x => x.Id).Column("ID");
            Map(x => x.Qatar).Column("QATAR");
            Map(x => x.Senegal).Column("SENEGAL");
            Map(x => x.Equador).Column("EQUADOR");
            Map(x => x.Holanda).Column("HOLANDA");
            Map(x => x.QatarPontos).Column("QATARPONTOS");
            Map(x => x.SenegalPontos).Column("SENEGALPONTOS");
            Map(x => x.EquadorPontos).Column("EQUADORPONTOS");
            Map(x => x.HolandaPontos).Column("HOLANDAPONTOS");
        }
    }
}
