using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class Ge_1Map : ClassMap<Ge_1>
    {
        public Ge_1Map()
        {
            Schema("teste");
            Table("ge_1");
            Id(x => x.Id).Column("ID");
            Map(x => x.Japao).Column("JAPAO");
            Map(x => x.Alemanha).Column("ALEMANHA");
            Map(x => x.Espanha).Column("ESPANHA");
            Map(x => x.CostaRica).Column("COSTARICA");
            Map(x => x.JapaoPontos).Column("JAPAOPONTOS");
            Map(x => x.AlemanhaPontos).Column("ALEMANHAPONTOS");
            Map(x => x.EspanhaPontos).Column("ESPANHAPONTOS");
            Map(x => x.CostaRicaPontos).Column("COSTARICAPONTOS");
        }
    }
}
