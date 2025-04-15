using System.ComponentModel.DataAnnotations;

namespace BolaoTeste.Dto.Cadastros
{
    public class ChecarUsuarioResponse
    {               
        public virtual int Id { get; set; }
        public virtual string Token { get; set; }
        

    }
}
