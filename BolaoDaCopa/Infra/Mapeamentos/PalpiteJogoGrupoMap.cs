using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteJogoGrupoMap : ClassMap<PalpiteJogoGrupo>
    {
        public PalpiteJogoGrupoMap()
        {
            Schema("bolao");
            Table("palpitejogogrupo");
            Id(x => x.Id).Column("id");
            Map(x => x.PlacarSelecao1).Column("placarselecao1");
            Map(x => x.PlacarSelecao2).Column("placarselecao2");
            References(x => x.Grupo).Column("idgrupo").Not.Nullable();
            References(x => x.BolaoUsuario).Column("idbolaousuario").Not.Nullable();
            References(x => x.Selecao1).Column("idselecao1").Not.Nullable();
            References(x => x.Selecao2).Column("idselecao2").Not.Nullable();
        }
    }
}
