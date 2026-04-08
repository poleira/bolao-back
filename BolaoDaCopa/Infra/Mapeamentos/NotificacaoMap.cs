using BolaoDaCopa.Models;
using BolaoDaCopa.Models.Enums;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamentos
{
    public class NotificacaoMap : ClassMap<Notificacao>
    {
        public NotificacaoMap()
        {
            Schema("bolao");
            Table("notificacao");
            Id(x => x.Id).Column("id");
            Map(x => x.Tipo).Column("tipo").Not.Nullable().CustomType<TipoMensagemEnum>();
            Map(x => x.Lida).Column("lida").Not.Nullable().Default("false");
            Map(x => x.Mensagem).Column("mensagem").Not.Nullable();
            Map(x => x.HashBolao).Column("hashbolao").Nullable();
            References(x => x.UsuarioRecebendo).Column("idusuario").Not.Nullable();
            References(x => x.UsuarioEnviando).Column("idusuarioenviando");
        }
    }
}
