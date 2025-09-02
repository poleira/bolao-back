using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class SelecaoMap : ClassMap<Selecao>
    {
        public SelecaoMap()
        {
            Schema("Bolao");
            Table("Selecao");
            Id(x => x.Id).Column("ID");
            Map(x => x.Nome).Column("Nome").Not.Nullable();
            Map(x => x.Logo).Column("Logo").Not.Nullable();
            Map(x => x.Abreviacao).Column("Abreviacao").Not.Nullable();
            Map(x => x.PosicaoFaseDeGrupos).Column("PosicaoFaseDeGrupos");
            References(x => x.Grupo).Column("IDGrupo").Not.Nullable();
            Map(x => x.PontuacaoSelecao).Column("PontuacaoSelecao");
        }
    }
}
