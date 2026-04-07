using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteTerceiroLugarMap : ClassMap<PalpiteTerceiroLugar>
    {
        public PalpiteTerceiroLugarMap()
        {
            Schema("bolao");
            Table("palpiteterceirolugar");
            Id(x => x.Id).Column("id");
            References(x => x.BolaoUsuario).Column("idbolaousuario").Not.Nullable();
            References(x => x.Selecao).Column("idselecao").Not.Nullable();
            Map(x => x.Posicao).Column("posicao");
        }
    }
}
