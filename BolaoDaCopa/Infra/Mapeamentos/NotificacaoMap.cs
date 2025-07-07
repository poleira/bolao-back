using BolaoDaCopa.Models;
using BolaoDaCopa.Models.Enums;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{
    public class NotificacaoMap : ClassMap<Notificacao>
    {
        public NotificacaoMap()
        {
            Schema("Bolao");
            Table("Notificacao");
            Id(x => x.Id).Column("ID");
            Map(x => x.Tipo).Column("Tipo").Not.Nullable().CustomType<TipoMensagemEnum>();
            Map(x => x.Lida).Column("Lida").Not.Nullable().Default("0").CustomType(typeof(NHibernate.Type.BooleanType)).Not.Nullable();
            Map(x => x.Mensagem).Column("Mensagem").Not.Nullable();
            Map(x => x.HashBolao).Column("HashBolao").Nullable();
            References(x => x.UsuarioRecebendo).Column("IDUsuario").Not.Nullable();
            References(x => x.UsuarioEnviando).Column("IDUsuarioEnviando").Not.Nullable();
        }
    }
}
