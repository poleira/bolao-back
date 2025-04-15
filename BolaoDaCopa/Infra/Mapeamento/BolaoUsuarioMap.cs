using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class BolaoUsuarioMap : ClassMap<BolaoUsuario>
    {
        public BolaoUsuarioMap()
        {
            Schema("Bolao");
            Table("BolaoUsuario");
            Id(x => x.Id).Column("ID");
            References(x => x.Usuario).Column("IDUsuario");
            References(x => x.Bolao).Column("IDBolao");
        }
    }
}
