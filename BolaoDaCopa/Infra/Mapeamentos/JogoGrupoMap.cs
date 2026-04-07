using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
        public class JogoGrupoMap : ClassMap<JogoGrupo>
        {
            public JogoGrupoMap()
            {
                Schema("bolao");
                Table("jogogrupo");
                Id(x => x.Id).Column("id");
                References(x => x.Grupo).Column("idgrupo").Not.Nullable();
                References(x => x.Selecao1).Column("idselecao1").Not.Nullable();
                References(x => x.Selecao2).Column("idselecao2").Not.Nullable();
                Map(x => x.PlacarSelecao1).Column("placarselecao1").Not.Nullable();
                Map(x => x.PlacarSelecao2).Column("placarselecao2").Not.Nullable();

            }
        }
    
}
