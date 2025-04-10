using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Oitavas_1Map : ClassMap<Oitavas_1>
    {
        public Oitavas_1Map()
        {
            Schema("teste");
            Table("oitavas_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.time1).Column("TIME1");
            Map(x => x.time2).Column("TIME2");
            Map(x => x.time3).Column("TIME3");
            Map(x => x.time4).Column("TIME4");
            Map(x => x.time5).Column("TIME5");
            Map(x => x.time6).Column("TIME6");
            Map(x => x.time7).Column("TIME7");
            Map(x => x.time8).Column("TIME8");
            Map(x => x.time9).Column("TIME9");
            Map(x => x.time10).Column("TIME10");
            Map(x => x.time11).Column("TIME11");
            Map(x => x.time12).Column("TIME12");
            Map(x => x.time13).Column("TIME13");
            Map(x => x.time14).Column("TIME14");
            Map(x => x.time15).Column("TIME15");
            Map(x => x.time16).Column("TIME16");

        }
    }
}
