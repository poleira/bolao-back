using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
        public class JogoGrupoMap : ClassMap<JogoGrupo>
        {
            public JogoGrupoMap()
            {
                Schema("Bolao");
                Table("JogoGrupo");
                Id(x => x.Id).Column("ID");
                References(x => x.Grupo).Column("IDGrupo").Not.Nullable();
                References(x => x.Selecao1).Column("IDSelecao1").Not.Nullable();
                References(x => x.Selecao2).Column("IDSelecao2").Not.Nullable();
                Map(x => x.PlacarSelecao1).Column("PlacarSelecao1").Not.Nullable();
                Map(x => x.PlacarSelecao2).Column("PlacarSelecao2").Not.Nullable();

            }
        }
    
}
