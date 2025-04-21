using BolaoDaCopa.Dto.Boloes.Comandos;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces
{
    public interface IBoloesUsuariosRepositorio
    {
        void Inserir(BolaoUsuarioComando bolaoUsuario);
        void Deletar(BolaoUsuarioComando bolaoUsuario);
    }
}
