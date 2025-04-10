using BolaoTeste.Models;
using FluentNHibernate.Mapping;
using System.Security.Cryptography.Xml;

namespace BolaoTeste.Data.Mapeamento
{
    public class CampeonatoMap : ClassMap<Campeonato>
    {
        public CampeonatoMap()
        {

            Schema("teste");
            Table("campeonato");
            Id(x => x.Codcampeonato).Column("CODCAMPEONATO");
            Map(x => x.NomeCampeonato).Column("NOME_CAMPEONATO");
            Map(x => x.Senha).Column("SENHA");
            Map(x => x.Usuario).Column("USUARIO");
            References(x => x.Ga_1).Column("IDGA_1").Cascade.All();
            References(x => x.Gb_1).Column("IDGB_1").Cascade.All();
            References(x => x.Gc_1).Column("IDGC_1").Cascade.All();
            References(x => x.Gd_1).Column("IDGD_1").Cascade.All();
            References(x => x.Ge_1).Column("IDGE_1").Cascade.All();
            References(x => x.Gf_1).Column("IDGF_1").Cascade.All();
            References(x => x.Gg_1).Column("IDGG_1").Cascade.All();
            References(x => x.Gh_1).Column("IDGH_1").Cascade.All();
            References(x => x.Oitavas_1).Column("IDOITAVAS_1").Cascade.All().LazyLoad();
            References(x => x.Quartas_1).Column("IDQUARTAS_1").Cascade.All().LazyLoad();
            References(x => x.Semis_1).Column("IDSEMIS_1").Cascade.All().LazyLoad();
            References(x => x.Finais_1).Column("IDFINAIS_1").Cascade.All().LazyLoad();
            References(x => x.Campeao_1).Column("IDCAMPEAO_1").Cascade.All().LazyLoad();
            References(x => x.Jogos_BR_1).Column("IDJOGOSDOBR_1").Cascade.All().LazyLoad();

        }
    }
}
