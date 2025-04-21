using AutoMapper;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoTeste.Dto;
using BolaoTeste.Dto.JogosBr;
using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Mvc;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Aplicacao.Palpites.Servicos
{
    public class PalpitesServico : IPalpitesServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly IBoloesRepositorio cadastroRepositorio;
        private readonly ISelecoesRepositorio palpiteRepositorio;

        public PalpitesServico(ISession session, IMapper mapper, IBoloesRepositorio cadastroRepositorio, ISelecoesRepositorio palpiteRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.cadastroRepositorio = cadastroRepositorio;
            this.palpiteRepositorio = palpiteRepositorio;
        }

        public OkResponse EditaCampeao(CampeaoEditarRequest request)
        {

            var transacao = session.BeginTransaction();
            try
            {


                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };

                return null;

            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }


    }

}

