using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteFaseSelecaoMap : ClassMap<PalpiteFaseSelecao>
    {
        public PalpiteFaseSelecaoMap()
        {
            Schema("bolao");
            Table("palpitefaseselecao");
            Id(x => x.Id).Column("id");
            References(x => x.Fase).Column("idfase").Not.Nullable();
            References(x => x.Selecao).Column("idselecao").Not.Nullable();
            References(x => x.BolaoUsuario).Column("idbolaousuario").Not.Nullable();
        }
    }
}