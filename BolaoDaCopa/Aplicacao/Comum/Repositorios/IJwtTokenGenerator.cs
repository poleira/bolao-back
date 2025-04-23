using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Comum.Repositorios
{
    public interface IJwtTokenGenerator
    {
        string GerarToken(Usuario usuario);
    }
}
