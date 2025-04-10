using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class GdMap : ClassMap<Gd>
    {
        public GdMap()
        {
            Schema("teste");
            Table("gd");
            Id(x => x.Id).Column("ID");
            Map(x => x.Franca).Column("FRANCA");
            Map(x => x.Australia).Column("AUSTRALIA");
            Map(x => x.Dinamarca).Column("DINAMARCA");
            Map(x => x.Tunisia).Column("TUNISIA");
            Map(x => x.FrancaPontos).Column("FRANCAPONTOS");
            Map(x => x.AustraliaPontos).Column("AUSTRALIAPONTOS");
            Map(x => x.DinamarcaPontos).Column("DINAMARCAPONTOS");
            Map(x => x.TunisiaPontos).Column("TUNISIAPONTOS");
        }
    }
}
