using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {

            Schema("bolao");
            Table("usuario");
            
            Id(x => x.Id).Column("id");
            Map(x => x.Nome).Column("nome");
            Map(x => x.FirebaseUid).Column("firebaseuid");
            Map(x => x.Email).Column("email");
            Map(x => x.Logo).Column("logo");
            
            HasManyToMany(x => x.Boloes)
                .Schema("bolao")
                .Table("bolaousuario")
                .ParentKeyColumn("idusuario")
                .ChildKeyColumn("idbolao")
                .Cascade.All();
        }
    }
}
