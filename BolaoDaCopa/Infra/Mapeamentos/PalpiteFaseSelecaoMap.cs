using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteFaseSelecaoMap : ClassMap<PalpiteFaseSelecao>
    {
        public PalpiteFaseSelecaoMap()
        {
            Schema("Bolao");
            Table("PalpiteFaseSelecao");
            Id(x => x.Id).Column("ID");
            References(x => x.Fase).Column("IDFase").Not.Nullable();
            References(x => x.Selecao).Column("IDSelecao").Not.Nullable();
            References(x => x.BolaoUsuario).Column("IDBolaoUsuario").Not.Nullable();
        }
    }
}