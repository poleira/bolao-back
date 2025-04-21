using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class ArtilheiroMap : ClassMap<Artilheiro>
    {
        public ArtilheiroMap()
        {
            Schema("Bolao");
            Table("Artilheiro");
            Id(x => x.Id).Column("ID");
            References(x => x.Jogador).Column("IDJogador").Not.Nullable();
        }
    }
}
