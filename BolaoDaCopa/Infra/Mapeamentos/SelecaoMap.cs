using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class SelecaoMap : ClassMap<Selecao>
    {
        public SelecaoMap()
        {
            Schema("bolao");
            Table("selecao");
            Id(x => x.Id).Column("id");
            Map(x => x.Nome).Column("nome").Not.Nullable();
            Map(x => x.Logo).Column("logo").Not.Nullable();
            Map(x => x.Abreviacao).Column("abreviacao").Not.Nullable();
            Map(x => x.PosicaoFaseDeGrupos).Column("posicaofasedegrupos");
            Map(x => x.SportsDbId).Column("sportsdbid");
            References(x => x.Grupo).Column("idgrupo").Not.Nullable();
            Map(x => x.PontuacaoSelecao).Column("pontuacaoselecao");
        }
    }
}
