using BolaoDaCopa.Aplicacao.ModosJogos.Servicos.Interfaces;
using BolaoDaCopa.Dto.Regras.Responses;
using BolaoDaCopa.Infra.Repositorios.ModoJogoRegra.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Regras.Interfaces;

namespace BolaoDaCopa.Aplicacao.ModosJogos.Servicos
{
    public class ModosJogosServicos : IModosJogosServicos
    {
        private readonly IModoJogoRegraRepositorio _modoJogoRegraRepositorio;
        private readonly IRegrasRepositorio _regrasRepositorio;

        public ModosJogosServicos(IModoJogoRegraRepositorio modoJogoRegraRepositorio, IRegrasRepositorio regrasRepositorio)
        {
            _modoJogoRegraRepositorio = modoJogoRegraRepositorio;
            _regrasRepositorio = regrasRepositorio;
        }

        public IEnumerable<RegraResponse> ListarRegrasModoJogo(int idModoJogo)
        {
            var query = _modoJogoRegraRepositorio.QueryModoJogoRegra()
                .Where(mr => mr.ModoJogo.Id == idModoJogo)
                .Select(mr => mr.Regra)
                .OrderBy(r => r.Id);

            return query.Select(r => new RegraResponse
            {
                Id = r.Id,
                Descricao = r.Descricao,
                Explicacao = r.Explicacao
            })
            .ToList();
        }
    }
}
