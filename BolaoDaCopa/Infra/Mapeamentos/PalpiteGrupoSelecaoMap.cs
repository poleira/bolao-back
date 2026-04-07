using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteGrupoSelecaoMap : ClassMap<PalpiteGrupoSelecao>
    {
        public PalpiteGrupoSelecaoMap()
        {
            Schema("bolao");
            Table("palpitegruposelecao");
            Id(x => x.Id).Column("id");
            Map(x => x.PontuacaoSelecao).Column("pontuacaoselecao");
            Map(x => x.PosicaoSelecao).Column("posicaoselecao");
            References(x => x.Grupo).Column("idgrupo").Not.Nullable();
            References(x => x.BolaoUsuario).Column("idbolaousuario").Not.Nullable();
            References(x => x.Selecao).Column("idselecao").Not.Nullable();
        }
    }
}
