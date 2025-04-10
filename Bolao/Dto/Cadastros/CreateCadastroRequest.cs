using System.ComponentModel.DataAnnotations;

namespace BolaoTeste.Data.Dto.Cadastros
{
    public class CreateCadastroRequest
    {
        public virtual string Usuario { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Senha { get; set; }
        
    }
}
