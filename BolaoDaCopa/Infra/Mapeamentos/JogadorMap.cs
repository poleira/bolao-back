using BolaoDaCopa.Models;
using FluentNHibernate.Mapping;

namespace BolaoDaCopa.Infra.Mapeamento
{
    public class JogadorMap : ClassMap<Jogador>
    {
        public JogadorMap() 
        {
            Schema("Bolao");
            Table("Jogador");
            Id(x => x.Id).Column("ID");
            Map(x => x.Nome).Column("Nome").Not.Nullable();
        }
    }
}
