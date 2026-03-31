using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteArtilheiroBrasilMap : ClassMap<PalpiteArtilheiroBrasil>
    {
        public PalpiteArtilheiroBrasilMap()
        {
            Schema("Bolao");
            Table("PalpiteArtilheiroBrasil");
            Id(x => x.Id).Column("ID");
            References(x => x.BolaoUsuario).Column("IDBolaoUsuario").Not.Nullable();
            References(x => x.Jogador).Column("IDJogador").Not.Nullable();
        }
    }
}
