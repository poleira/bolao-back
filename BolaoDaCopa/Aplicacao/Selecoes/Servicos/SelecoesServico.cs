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
        public IList<GrupoSelecaoResponse> ListarGruposSelecoes(GrupoSelecaoRequest request)
        {
            var query = selecoesRepositorio.QueryGrupoSelecao();

            if (!string.IsNullOrEmpty(request.Grupo))
            {
                query = query.Where(x => x.Grupo.Nome == request.Grupo.ToUpper());
            }

            var projecao = query
                .Select(x => new GrupoSelecaoResponse
                {
                    Id = x.Id,
                    Nome = x.Selecao.Nome,
                    Grupo = x.Grupo.Nome,
                    Logo = x.Selecao.Logo,
                    Abreviacao = x.Selecao.Abreviacao,
                    Pontuacao = x.PontuacaoSelecao,
                });

            return projecao.ToList();
        }
    }
}
