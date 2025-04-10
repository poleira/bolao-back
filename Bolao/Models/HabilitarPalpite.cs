namespace BolaoTeste.Models
{
    public class HabilitarPalpite
    {
        public HabilitarPalpite()
        {
        }

        public virtual int Id { get; set; }
        public virtual bool Geral { get; set; }
        public virtual bool Oitavas { get; set; }
        public virtual bool Quartas { get; set; }
        public virtual bool Semis { get; set; }
        public virtual bool Finais { get; set; }
    }


}
