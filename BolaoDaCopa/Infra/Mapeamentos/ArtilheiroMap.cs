using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class ArtilheiroMap : ClassMap<Artilheiro>
    {
        public ArtilheiroMap()
        {
            Schema("bolao");
            Table("artilheiro");
            Id(x => x.Id).Column("id");
            References(x => x.Jogador).Column("idjogador").Not.Nullable();
        }
    }
}
