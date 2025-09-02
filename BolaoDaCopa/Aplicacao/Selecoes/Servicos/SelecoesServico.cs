using AutoMapper;
using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Dto.Selecoes.Requests;
using BolaoDaCopa.Dto.Selecoes.Responses;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Models;
using FluentNHibernate.Conventions;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.HabilitarPalpites.Servicos
{
    public class SelecoesServico : ISelecoesServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly ISelecoesRepositorio selecoesRepositorio;

        public SelecoesServico(ISession session, IMapper mapper, ISelecoesRepositorio selecoesRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.selecoesRepositorio = selecoesRepositorio;
        }
        public IList<GrupoSelecaoResponse> ListarSelecoes(GrupoSelecaoRequest request)
        {
            IQueryable<Selecao> query = selecoesRepositorio.QuerySelecao();

            if (!string.IsNullOrEmpty(request.Grupo))
            {
                query = query.Where(x => x.Grupo.Nome == request.Grupo.ToUpper());
            }

            var projecao = query
                .Select(x => new GrupoSelecaoResponse
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Grupo = new GrupoResponse
                    {
                        Id = x.Grupo.Id,
                        Nome = x.Grupo.Nome
                    },
                    Logo = x.Logo,
                    Abreviacao = x.Abreviacao,
                    Pontuacao = x.PontuacaoSelecao,
                    PosicaoFaseDeGrupos = x.PosicaoFaseDeGrupos
                });

            return projecao.ToList();
        }
    }
}
