﻿using FluentNHibernate.Mapping;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class BolaoMap : ClassMap<Bolao>
    {
        public BolaoMap()
        {
            Schema("Bolao");
            Table("Bolao");
            Id(x => x.Id).Column("ID");
            Map(x => x.Nome).Column("Nome");
            Map(x => x.Logo).Column("Logo");
            Map(x => x.TokenAcesso).Column("TokenAcesso");
            Map(x => x.Aviso).Column("Aviso");
            References(x => x.UsuarioAdm).Column("IDUsuarioAdm").Not.Nullable();
            HasManyToMany(x => x.Usuarios)
                .Table("BolaoUsuario")
                .ParentKeyColumn("IDBolao")
                .ChildKeyColumn("IDUsuario")
                .Cascade.All();
            HasMany(x => x.Regras)
                .KeyColumn("IDBolao")
                .Cascade.All();
        }
    }
}
