using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {

            Schema("Bolao");
            Table("Usuario");
            Id(x => x.Id).Column("ID");
            Map(x => x.Nome).Column("Nome");
            Map(x => x.Senha).Column("Senha");
            Map(x => x.Email).Column("Email");
            Map(x => x.Logo).Column("Logo");
            HasManyToMany(x => x.Boloes)
                .Table("BolaoUsuario")
                .ParentKeyColumn("IDUsuario")
                .ChildKeyColumn("IDBolao")
                .Cascade.All();
        }
    }
}
