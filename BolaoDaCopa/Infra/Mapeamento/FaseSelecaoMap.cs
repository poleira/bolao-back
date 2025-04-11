using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
        public class FaseSelecaoMap : ClassMap<FaseSelecao>
        {
            public FaseSelecaoMap()
            {
                Schema("Bolao");
                Table("FaseSelecao");
                Id(x => x.Id).Column("ID");
                References(x => x.Fase).Column("IDFase").Not.Nullable();
                References(x => x.Selecao).Column("IDSelecao").Not.Nullable();
            }
        }
    
}
