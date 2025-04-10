using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Gg_1Map : ClassMap<Gg_1>
    {
        public Gg_1Map()
        {
            Schema("teste");
            Table("gg_1");
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
