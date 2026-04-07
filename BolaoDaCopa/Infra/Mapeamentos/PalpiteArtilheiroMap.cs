using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteArtilheiroMap : ClassMap<PalpiteArtilheiro>
    {
        public PalpiteArtilheiroMap()
        {
            Schema("bolao");
            Table("palpiteartilheiro");
            Id(x => x.Id).Column("id");
            References(x => x.BolaoUsuario).Column("idbolaousuario").Not.Nullable();
            References(x => x.Jogador).Column("idjogador").Not.Nullable();
        }
    }
}
        
