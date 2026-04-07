using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class BolaoUsuarioMap : ClassMap<BolaoUsuario>
    {
        public BolaoUsuarioMap()
        {
            Schema("bolao");
            Table("bolaousuario");
            Id(x => x.Id).Column("id");
            References(x => x.Usuario).Column("idusuario");
            References(x => x.Bolao).Column("idbolao");
        }
    }
}
