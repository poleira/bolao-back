namespace BolaoDaCopa.Models
{
    public class Premio
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual Bolao Bolao { get; set; }
        public virtual int Colocacao { get; set; }

        protected Premio() { }

        public Premio(string descricao, Bolao bolao, int colocacao)
        {
            Descricao = descricao;
            Bolao = bolao;
            Colocacao = colocacao;
        }
    }
}
