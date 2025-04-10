using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
        public class Gb_1Map : ClassMap<Gb_1>
        {
            public Gb_1Map()
            {
                Schema("teste");
                Table("gb_1");
                Id(x => x.Id).Column("ID");
                Map(x => x.PaisDeGales).Column("PAISDEGALES");
                Map(x => x.Iram).Column("IRAM");
                Map(x => x.Usa).Column("USA");
                Map(x => x.Inglaterra).Column("INGLATERRA");
                Map(x => x.PaisDeGalesPontos).Column("PAISDEGALESPONTOS");
                Map(x => x.IramPontos).Column("IRAMPONTOS");
                Map(x => x.UsaPontos).Column("USAPONTOS");
                Map(x => x.InglaterraPontos).Column("INGLATERRAPONTOS");
        }
        }
    
}
