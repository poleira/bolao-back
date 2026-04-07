using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class PalpiteArtilheiroBrasilMap : ClassMap<PalpiteArtilheiroBrasil>
    {
        public PalpiteArtilheiroBrasilMap()
        {
            Schema("bolao");
            Table("palpiteartilheirobrasil");
            Id(x => x.Id).Column("id");
            References(x => x.BolaoUsuario).Column("idbolaousuario").Not.Nullable();
            References(x => x.Jogador).Column("idjogador").Not.Nullable();
        }
    }
}
