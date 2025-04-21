using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteArtilheiroMap : ClassMap<PalpiteArtilheiro>
    {
        public PalpiteArtilheiroMap()
        {
            Schema("Bolao");
            Table("PalpiteArtilheiro");
            Id(x => x.Id).Column("ID");
            References(x => x.BolaoUsuario).Column("IDBolaoUsuario").Not.Nullable();
            References(x => x.Jogador).Column("IDJogador").Not.Nullable();
        }
    }
}
        
