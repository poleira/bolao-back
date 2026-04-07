using FluentNHibernate.Mapping;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class BolaoMap : ClassMap<Bolao>
    {
        public BolaoMap()
        {
            Schema("bolao");
            Table("bolao");
            Id(x => x.Id).Column("id");
            Map(x => x.Nome).Column("nome").Not.Nullable();
            Map(x => x.Logo).Column("logo").Not.Nullable();
            Map(x => x.TokenAcesso).Column("tokenacesso");
            Map(x => x.Aviso).Column("aviso");
            Map(x => x.Privado).Column("privacidade").Not.Nullable();
            References(x => x.UsuarioAdm).Column("idusuarioadm").Not.Nullable();
            Map(x => x.Senha).Column("senha").Nullable();
            HasManyToMany(x => x.Usuarios)
                .Schema("bolao")
                .Table("bolaousuario")
                .ParentKeyColumn("idbolao")
                .ChildKeyColumn("idusuario")
                .Cascade.All().Not.LazyLoad();
            
            HasMany(x => x.Regras)
                .KeyColumn("idbolao")
                .Cascade.All().Not.LazyLoad();

            References(x => x.ModoJogo).Column("idmodojogo").Nullable();

            HasMany(x => x.Premios)
                .KeyColumn("idbolao")
                .Cascade.All().Not.LazyLoad();
        }
    }
}
