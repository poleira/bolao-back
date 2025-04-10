using BolaoTeste.Models;
using FluentNHibernate.Mapping;
using System.Security.Cryptography.Xml;

namespace BolaoTeste.Data.Mapeamento
{
    public class CadastroMap : ClassMap<Cadastro>
    {
        public CadastroMap()
        {

            Schema("teste");
            Table("cadastro");
            Id(x => x.Id).Column("CODCADASTRO");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.Senha).Column("SENHA");
            Map(x => x.Usuario).Column("USUARIO");
            Map(x => x.Pontuacao).Column("PONTUACAO");
            References(x => x.Ga).Column("IDGA").Cascade.All();
            References(x => x.Gb).Column("IDGB").Cascade.All();
            References(x => x.Gc).Column("IDGC").Cascade.All();
            References(x => x.Gd).Column("IDGD").Cascade.All();
            References(x => x.Ge).Column("IDGE").Cascade.All();
            References(x => x.Gf).Column("IDGF").Cascade.All();
            References(x => x.Gg).Column("IDGG").Cascade.All();
            References(x => x.Gh).Column("IDGH").Cascade.All();
            References(x => x.Oitavas).Column("IDOITAVAS").Cascade.All().LazyLoad();
            References(x => x.Quartas).Column("IDQUARTAS").Cascade.All().LazyLoad();
            References(x => x.Semis).Column("IDSEMIS").Cascade.All().LazyLoad();
            References(x => x.Finais).Column("IDFINAIS").Cascade.All().LazyLoad();
            References(x => x.Campeao).Column("IDCAMPEAO").Cascade.All().LazyLoad();
            References(x => x.Jogos_BR).Column("IDJOGOSDOBR").Cascade.All().LazyLoad();

        }
    }
}
