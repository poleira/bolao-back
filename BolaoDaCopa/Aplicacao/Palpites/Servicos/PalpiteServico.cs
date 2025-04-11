using AutoMapper;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Data.Interfaces;
using BolaoTeste.Data.Repositorios.Interfaces;
using BolaoTeste.Dto;
using BolaoTeste.Dto.JogosBr;
using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Palpites;
using Microsoft.AspNetCore.Mvc;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Aplicacao.Palpites.Servicos
{
    public class PalpiteServico : IPalpiteServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly ICadastroRepositorio cadastroRepositorio;
        private readonly IPalpiteRepositorio palpiteRepositorio;

        public PalpiteServico(ISession session, IMapper mapper, ICadastroRepositorio cadastroRepositorio, IPalpiteRepositorio palpiteRepositorio)
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

