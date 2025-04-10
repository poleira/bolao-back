using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Gf_1Map : ClassMap<Gf_1>
    {
        public Gf_1Map()
        {
            Schema("teste");
            Table("gf_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.Croacia).Column("CROACIA");
            Map(x => x.Canada).Column("CANADA");
            Map(x => x.Marrocos).Column("MARROCOS");
            Map(x => x.Belgica).Column("BELGICA");
            Map(x => x.CroaciaPontos).Column("CROACIAPONTOS");
            Map(x => x.CanadaPontos).Column("CANADAPONTOS");
            Map(x => x.MarrocosPontos).Column("MARROCOSPONTOS");
            Map(x => x.BelgicaPontos).Column("BELGICAPONTOS");
        }
    }
}
