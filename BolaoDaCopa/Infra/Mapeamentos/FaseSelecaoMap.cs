using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
        public class FaseSelecaoMap : ClassMap<FaseSelecao>
        {
            public FaseSelecaoMap()
            {
                Schema("bolao");
                Table("faseselecao");
                Id(x => x.Id).Column("id");
                References(x => x.Selecao).Column("idselecao").Not.Nullable();
            }
        }
    
}
