using AutoMapper;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Fases.Responses;
using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Palpites.Interface;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;
using NHibernate.Linq;
using System.Threading.Tasks;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.Palpites.Servicos
{
    public class PalpitesServico : IPalpitesServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly IPalpitesRepositorio palpiteRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;
        private readonly IJogadoresRepositorio jogadoresRepositorio;
        private readonly ISelecoesRepositorio selecoesRepositorio;

        public PalpitesServico(ISession session, IMapper mapper, IPalpitesRepositorio palpiteRepositorio, IUsuariosRepositorio usuariosRepositorio, IBoloesUsuariosRepositorio boloesUsuariosRepositorio, IJogadoresRepositorio jogadoresRepositorio, ISelecoesRepositorio selecoesRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.palpiteRepositorio = palpiteRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
            this.jogadoresRepositorio = jogadoresRepositorio;
            this.selecoesRepositorio = selecoesRepositorio;
        }

        public async Task CriarPalpiteArtilheiro(CriarPalpiteArtilheiroRequest request)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.HashBolao));
                Usuario usuario = usuariosRepositorio.Recuperar(request.IdUsuario) ?? throw new Exception("Usuário não encontrado.");
                Jogador jogador = jogadoresRepositorio.Recuperar(request.JogadorId) ?? throw new Exception("Jogador não encontrado.");

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                await palpiteRepositorio.InserirPalpiteArtilheiro(new PalpiteArtilheiro(jogador, bolaoUsuario));
                

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

        public async Task CriarPalpiteFaseSelecao(CriarPalpiteFaseSelecaoRequest[] request, int idUsuario)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                await palpiteRepositorio.DeletarPalpiteFaseSelecaoPorBolaoUsuario(bolaoUsuario.Id);

                foreach (var item in request)
                {
                    var queryFase = selecoesRepositorio.RecuperarQueryFasePorId(item.IdFase) ?? throw new Exception("Fase não encontrada.");
                    var projecaoFase = (await queryFase.Select(x => new FaseResponse
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                    }).ToListAsync()).FirstOrDefault() ?? throw new Exception("Fase não encontrada.");

                    Selecao selecao = selecoesRepositorio.Recuperar(item.IdSelecao) ?? throw new Exception("Seleção não encontrada.");

                    await palpiteRepositorio.InserirPalpiteFaseSelecao(new PalpiteFaseSelecao(new Fase(projecaoFase.Id, projecaoFase.Nome), selecao, bolaoUsuario));
                }

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

        public async Task CriarPalpiteGrupoSelecao(CriarPalpiteGrupoSelecaoRequest[] request, int idUsuario)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                await palpiteRepositorio.DeletarPalpiteGrupoSelecaoPorBolaoUsuario(bolaoUsuario.Id);

                foreach (var item in request)
                {
                    Selecao selecao = selecoesRepositorio.Recuperar(item.IdSelecao) ?? throw new Exception("Seleção não encontrada.");
                    Grupo grupo = await selecoesRepositorio.RecuperarGrupo(item.IdGrupo) ?? throw new Exception("Grupo não encontrado.");
                    await palpiteRepositorio.InserirPalpiteGrupoSelecao(new PalpiteGrupoSelecao(grupo, selecao,item.PontuacaoSelecao, bolaoUsuario));
                }

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

        public async Task CriarPalpiteJogoGrupo(CriarPalpiteJogoGrupoRequest[] request, int idUsuario)
        {
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                await palpiteRepositorio.DeletarPalpiteJogoGrupoPorBolaoUsuario(bolaoUsuario.Id);

                foreach (var item in request)
                {
                    Selecao selecao1 = selecoesRepositorio.Recuperar(item.IdSelecao1) ?? throw new Exception("Seleção não encontrada.");
                    Selecao selecao2 = selecoesRepositorio.Recuperar(item.IdSelecao2) ?? throw new Exception("Seleção não encontrada.");
                    Grupo grupo = await selecoesRepositorio.RecuperarGrupo(item.IdGrupo) ?? throw new Exception("Grupo não encontrado.");
                    await palpiteRepositorio.InserirPalpiteJogoGrupo(new PalpiteJogoGrupo(grupo, selecao1, selecao2, item.PlacarSelecao1, item.PlacarSelecao2, bolaoUsuario));
                }

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

        public async Task<PalpiteArtilheiroResponse> RecuperarPalpiteArtilheiroAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                IQueryable<PalpiteArtilheiro> query = palpiteRepositorio.RecuperarQueryPalpiteArtilheiroPorBolaoUsuarioId(bolaoUsuario.Id);

                var projecao = query.Select(x => new PalpiteArtilheiroResponse
                {
                    Id = x.Id,
                    JogadorNome = x.Jogador.Nome,
                    JogadorId = x.Jogador.Id,
                });

                return (await projecao.ToListAsync()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar", ex);
            }
        }

        public async Task<IList<PalpiteFaseSelecaoResponse>> RecuperarPalpiteFaseSelecaoAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                IQueryable<PalpiteFaseSelecao> query = palpiteRepositorio.RecuperarQueryPalpiteFaseSelecaoPorBolaoUsuarioId(bolaoUsuario.Id);

                var projecao = query.Select(x => new PalpiteFaseSelecaoResponse
                {
                    Id = x.Id,
                    Fase = new FaseResponse
                    {
                        Id = x.Fase.Id,
                        Nome = x.Fase.Nome
                    },
                    Selecao = x.Selecao,
                });

                return await projecao.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar", ex);
            }
        }

        public async Task<IList<PalpiteGrupoSelecaoResponse>> RecuperarPalpiteGrupoSelecaoAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                IQueryable<PalpiteGrupoSelecao> query = palpiteRepositorio.RecuperarQueryPalpiteGrupoSelecaoPorBolaoUsuarioId(bolaoUsuario.Id);

                var projecao = query.Select(x => new PalpiteGrupoSelecaoResponse
                {
                    Id = x.Id,
                    Selecao = x.Selecao,
                    Grupo = x.Grupo,
                    PontuacaoSelecao = x.PontuacaoSelecao
                });

                return await projecao.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar", ex);
            }
        }

        public async Task<IList<PalpiteJogoGrupoResponse>> RecuperarPalpiteJogoGrupoAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                IQueryable<PalpiteJogoGrupo> query = palpiteRepositorio.RecuperarQueryPalpiteJogoGrupoPorBolaoUsuarioId(bolaoUsuario.Id);

                var projecao = query.Select(x => new PalpiteJogoGrupoResponse
                {
                    Id = x.Id,
                    Grupo = x.Grupo,
                    Selecao1 = x.Selecao1,
                    Selecao2 = x.Selecao2,
                    PlacarSelecao1 = x.PlacarSelecao1,
                    PlacarSelecao2 = x.PlacarSelecao2
                });

                return await projecao.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar", ex);
            }
        }

    }

}

