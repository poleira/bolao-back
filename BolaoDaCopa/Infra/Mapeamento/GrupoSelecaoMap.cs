using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class GrupoSelecaoMap : ClassMap<GrupoSelecao>
    {
        public GrupoSelecaoMap()
        {
            Schema("Bolao");
            Table("GrupoSelecao");
            Id(x => x.Id).Column("ID");
            References(x => x.Grupo).Column("IDGrupo").Not.Nullable();
            References(x => x.Selecao).Column("IDSelecao").Not.Nullable();
            Map(x => x.PontuacaoSelecao).Column("PontuacaoSelecao").Not.Nullable();

        }
    }
}
