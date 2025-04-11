using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteGrupoSelecaoMap : ClassMap<PalpiteGrupoSelecao>
    {
        public PalpiteGrupoSelecaoMap()
        {
            Schema("Bolao");
            Table("PalpiteGrupoSelecao");
            Id(x => x.Id).Column("ID");
            Map(x => x.PontuacaoSelecao).Column("PontuacaoSelecao");
            References(x => x.Grupo).Column("IDGrupo").Not.Nullable();
            References(x => x.BolaoUsuario).Column("IDBolaoUsuario").Not.Nullable();
            References(x => x.Selecao).Column("IDSelecao").Not.Nullable();
        }
    }
}
