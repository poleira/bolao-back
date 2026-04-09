using BolaoDaCopa.Aplicacao.JogosGrupo.Servicos.Interfaces;
using BolaoDaCopa.Dto.JogosGrupo.Requests;
using BolaoDaCopa.Dto.JogosGrupo.Responses;
using BolaoDaCopa.Infra.Repositorios.JogosGrupo.Interfaces;

namespace BolaoDaCopa.Aplicacao.JogosGrupo.Servicos
{
    public class JogosGrupoServico : IJogosGrupoServico
    {
        private readonly IJogosGrupoRepositorio jogosGrupoRepositorio;

        public JogosGrupoServico(IJogosGrupoRepositorio jogosGrupoRepositorio)
        {
            this.jogosGrupoRepositorio = jogosGrupoRepositorio;
        }

        public IList<JogoGrupoResponse> ListarJogosGrupo(JogoGrupoRequest request)
        {
            var query = jogosGrupoRepositorio.Query();

            if (!string.IsNullOrEmpty(request.Grupo))
            {
                query = query.Where(x => x.Grupo.Nome == request.Grupo.ToUpper());
            }

            var projecao = query.Select(x => new JogoGrupoResponse
            {
                Id = x.Id,
                Grupo = new GrupoJogoResponse
                {
                    Id = x.Grupo.Id,
                    Nome = x.Grupo.Nome
                },
                Selecao1 = new SelecaoJogoResponse
                {
                    Id = x.Selecao1.Id,
                    Nome = x.Selecao1.Nome,
                    Abreviacao = x.Selecao1.Abreviacao,
                    Logo = x.Selecao1.Logo
                },
                Selecao2 = new SelecaoJogoResponse
                {
                    Id = x.Selecao2.Id,
                    Nome = x.Selecao2.Nome,
                    Abreviacao = x.Selecao2.Abreviacao,
                    Logo = x.Selecao2.Logo
                },
                PlacarSelecao1 = x.PlacarSelecao1,
                PlacarSelecao2 = x.PlacarSelecao2
            });

            return projecao.ToList();
        }

        public IList<JogoGrupoResponse> ListarJogosBrasil()
        {
            var query = jogosGrupoRepositorio.Query()
                .Where(x => x.Selecao1.Abreviacao == "BRA" || x.Selecao2.Abreviacao == "BRA");

            var projecao = query.Select(x => new JogoGrupoResponse
            {
                Id = x.Id,
                Grupo = new GrupoJogoResponse
                {
                    Id = x.Grupo.Id,
                    Nome = x.Grupo.Nome
                },
                Selecao1 = new SelecaoJogoResponse
                {
                    Id = x.Selecao1.Id,
                    Nome = x.Selecao1.Nome,
                    Abreviacao = x.Selecao1.Abreviacao,
                    Logo = x.Selecao1.Logo
                },
                Selecao2 = new SelecaoJogoResponse
                {
                    Id = x.Selecao2.Id,
                    Nome = x.Selecao2.Nome,
                    Abreviacao = x.Selecao2.Abreviacao,
                    Logo = x.Selecao2.Logo
                },
                PlacarSelecao1 = x.PlacarSelecao1,
                PlacarSelecao2 = x.PlacarSelecao2
            });

            return projecao.ToList();
        }
    }
}
