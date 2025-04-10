using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class GgMap : ClassMap<Gg>
    {
        public GgMap()
        {
            Schema("teste");
            Table("gg");
            Id(x => x.Id).Column("ID");
            Map(x => x.Servia).Column("SERVIA");
            Map(x => x.Camaroes).Column("CAMAROES");
            Map(x => x.Suica).Column("SUICA");
            Map(x => x.Brasil).Column("BRASIL");
            Map(x => x.ServiaPontos).Column("SERVIAPONTOS");
            Map(x => x.CamaroesPontos).Column("CAMAROESPONTOS");
            Map(x => x.SuicaPontos).Column("SUICAPONTOS");
            Map(x => x.BrasilPontos).Column("BRASILPONTOS");
        }
    }
}
