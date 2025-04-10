using System.ComponentModel.DataAnnotations;

namespace BolaoTeste.Data.Dto.Cadastros
{
    public class CreateCadastroResponse
    {
        

        public virtual int Id { get; set; }
        public virtual string? Nome { get; set; }       
        public virtual string? Senha { get; set; }
    }
}
