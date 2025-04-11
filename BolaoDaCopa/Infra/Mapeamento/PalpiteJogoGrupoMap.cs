using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteJogoGrupoMap : ClassMap<PalpiteJogoGrupo>
    {
        public PalpiteJogoGrupoMap()
        {
            Schema("Bolao");
            Table("PalpiteJogoGrupo");
            Id(x => x.Id).Column("ID");
            Map(x => x.PlacarSelecao1).Column("PlacarSelecao1");
            Map(x => x.PlacarSelecao2).Column("PlacarSelecao2");
            References(x => x.Grupo).Column("IDGrupo").Not.Nullable();
            References(x => x.BolaoUsuario).Column("IDBolaoUsuario").Not.Nullable();
            References(x => x.Selecao1).Column("IDSelecao1").Not.Nullable();
            References(x => x.Selecao2).Column("IDSelecao2").Not.Nullable();
        }
    }
}
