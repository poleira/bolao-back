using BolaoTeste.Models;
using FluentNHibernate.Mapping;

namespace BolaoTeste.Data.Mapeamento
{
    public class GhMap : ClassMap<Gh>
    {
        public GhMap()
        {
            Schema("teste");
            Table("gh");
            Id(x => x.Id).Column("ID");
            Map(x => x.CoreiaDoSul).Column("COREIADOSUL");
            Map(x => x.Portugal).Column("PORTUGAL");
            Map(x => x.Uruguai).Column("URUGUAI");
            Map(x => x.Gana).Column("GANA");
            Map(x => x.CoreiaDoSulPontos).Column("COREIADOSULPONTOS");
            Map(x => x.PortugalPontos).Column("PORTUGALPONTOS");
            Map(x => x.UruguaiPontos).Column("URUGUAIPONTOS");
            Map(x => x.GanaPontos).Column("GANAPONTOS");
        }
    }
}
