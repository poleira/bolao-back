
namespace BolaoTeste.Models
{
    public class Cadastro
    {
        
        public virtual int Id { get; set; }
        public virtual string Usuario { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Senha { get; set; }
        public virtual int? Pontuacao { get; set; }
        public virtual Ga Ga { get; set; }
        public virtual Gb Gb { get; set; }
        public virtual Gc Gc { get; set; }
        public virtual Gd Gd { get; set; }
        public virtual Ge Ge { get; set; }
        public virtual Gf Gf { get; set; }
        public virtual Gg Gg { get; set; }
        public virtual Gh Gh { get; set; }
        public virtual Oitavas Oitavas { get; set; }
        public virtual Quartas Quartas { get; set; }
        public virtual Semis Semis { get; set; }
        public virtual Finais Finais { get; set; }
        public virtual Jogos_BR Jogos_BR { get; set; }
        public virtual Campeao Campeao { get; set; }

        public Cadastro(){}
    }
}
