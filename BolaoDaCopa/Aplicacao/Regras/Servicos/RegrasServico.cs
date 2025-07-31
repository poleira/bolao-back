using BolaoDaCopa.Aplicacao.Regras.Servicos.Interfaces;
using BolaoDaCopa.Dto.Regras.Responses;
using BolaoDaCopa.Infra.Repositorios.Regras.Interfaces;

namespace BolaoDaCopa.Aplicacao.Regras.Servicos
{
    public class RegrasServico : IRegrasServico
    {
        private readonly IRegrasRepositorio _regrasRepositorio;

        public RegrasServico(IRegrasRepositorio _regrasRepositorio)
        {
            this._regrasRepositorio = _regrasRepositorio;
        }

        public IEnumerable<RegraResponse> Listar()
        {
            var query =
                _regrasRepositorio
                .Query()
                .OrderBy(r => r.Id);

            return query.Select(r => new RegraResponse { 
                Id = r.Id,
                Descricao = r.Descricao, 
                Explicacao = r.Explicacao 
            })
            .ToList();
        }
    }
}
