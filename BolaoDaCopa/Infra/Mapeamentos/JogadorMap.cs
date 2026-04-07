using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class JogadorMap : ClassMap<Jogador>
    {
        public JogadorMap() 
        {
            Schema("bolao");
            Table("jogador");
            Id(x => x.Id).Column("id");
            Map(x => x.Nome).Column("nome").Not.Nullable();
        }
    }
}
