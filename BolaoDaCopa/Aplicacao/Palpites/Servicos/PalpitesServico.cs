using AutoMapper;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoDaCopa.Infra.Repositorios.Boloes;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Palpites.Interface;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;
using BolaoTeste.Dto;
using BolaoTeste.Dto.ListarPalpite;
using Microsoft.AspNetCore.Mvc;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Aplicacao.Palpites.Servicos
{
    public class PalpitesServico : IPalpitesServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly IBoloesRepositorio boloesRepositorio;
        private readonly IPalpitesRepositorio palpiteRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;
        private readonly IJogadoresRepositorio jogadoresRepositorio;

        public PalpitesServico(ISession session, IMapper mapper, IBoloesRepositorio boloesRepositorio, IPalpitesRepositorio palpiteRepositorio, IUsuariosRepositorio usuariosRepositorio, IBoloesUsuariosRepositorio boloesUsuariosRepositorio, IJogadoresRepositorio jogadoresRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.boloesRepositorio = boloesRepositorio;
            this.palpiteRepositorio = palpiteRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
            this.jogadoresRepositorio = jogadoresRepositorio;
        }

        public void CriarPalpiteArtilheiro(CriarPalpiteArtilheiroRequest request)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.HashBolao));
                Usuario usuario = usuariosRepositorio.RecuperarPorHash(request.HashUsuario) ?? throw new Exception("Usuário não encontrado.");
                Jogador jogador = jogadoresRepositorio.Recuperar(request.JogadorId) ?? throw new Exception("Jogador não encontrado.");

                BolaoUsuario bolaoUsuario = boloesUsuariosRepositorio.Recuperar(idBolao, usuario.Id);

                palpiteRepositorio.InserirPalpiteArtilheiro(new PalpiteArtilheiro(jogador, bolaoUsuario));

                if (transacao.IsActive)
                    transacao.Commit();
            }
            catch (Exception ex)
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                throw new Exception("Erro", ex);
            }
        }

        public async Task CriarPalpiteFaseSelecao(CriarPalpiteFaseSelecaoRequest[] request)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.RecuperarPorHash(request.First().HashUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = boloesUsuariosRepositorio.Recuperar(idBolao, usuario.Id);


                if (transacao.IsActive)
                    transacao.Commit();
            }
            catch (Exception ex)
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                throw new Exception("Erro", ex);
            }
        }

        public async Task CriarPalpiteGrupoSelecao(CriarPalpiteGrupoSelecaoRequest[] request)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.RecuperarPorHash(request.First().HashUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = boloesUsuariosRepositorio.Recuperar(idBolao, usuario.Id);


                if (transacao.IsActive)
                    transacao.Commit();
            }
            catch (Exception ex)
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                throw new Exception("Erro", ex);
            }
        }

        public async Task CriarPalpiteJogoGrupo(CriarPalpiteJogoGrupoRequest[] request)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.RecuperarPorHash(request.First().HashUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = boloesUsuariosRepositorio.Recuperar(idBolao, usuario.Id);


                if (transacao.IsActive)
                    transacao.Commit();
            }
            catch (Exception ex)
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                throw new Exception("Erro", ex);
            }
        }

    }

}

