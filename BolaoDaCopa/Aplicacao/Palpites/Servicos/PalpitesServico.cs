using AutoMapper;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Dto.Fases.Responses;
using BolaoDaCopa.Dto.Palpite.Requests;
using BolaoDaCopa.Dto.Palpite.Responses;
using BolaoDaCopa.Dto.Selecoes.Responses;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Palpites.Interface;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;
using NHibernate.Linq;
using System.Threading.Tasks;
using System.Text.Json;
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

        private static readonly DateTime DataLimitePalpites = new DateTime(2026, 6, 11);

        private static void ValidarPrazoParaPalpite()
        {
            if (DateTime.UtcNow.Date >= DataLimitePalpites.Date)
                throw new InvalidOperationException("O prazo para realizar palpites encerrou. Os palpites só são permitidos até o dia 10 de junho de 2026.");
        }

        public async Task CriarPalpiteArtilheiro(CriarPalpiteArtilheiroRequest request)
        {
            ValidarPrazoParaPalpite();
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
            ValidarPrazoParaPalpite();
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
            ValidarPrazoParaPalpite();
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                await palpiteRepositorio.DeletarPalpiteFaseSelecaoPorBolaoUsuario(bolaoUsuario.Id);
                await palpiteRepositorio.DeletarPalpiteTerceiroLugarPorBolaoUsuario(bolaoUsuario.Id);
                await palpiteRepositorio.DeletarPalpiteGrupoSelecaoPorBolaoUsuario(bolaoUsuario.Id);

                foreach (var item in request)
                {
                    Selecao selecao = selecoesRepositorio.Recuperar(item.IdSelecao) ?? throw new Exception("Seleção não encontrada.");
                    Grupo grupo = await selecoesRepositorio.RecuperarGrupo(item.IdGrupo) ?? throw new Exception("Grupo não encontrado.");
                    await palpiteRepositorio.InserirPalpiteGrupoSelecao(new PalpiteGrupoSelecao(grupo, selecao, item.PontuacaoSelecao.HasValue ? item.PontuacaoSelecao.Value : null, bolaoUsuario, item.PosicaoSelecao));
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
            ValidarPrazoParaPalpite();
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

        public async Task CriarPalpiteTerceiroLugar(CriarPalpiteTerceiroLugarRequest[] request, int idUsuario)
        {
            ValidarPrazoParaPalpite();
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.First().HashBolao));
                Usuario usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                await palpiteRepositorio.DeletarPalpiteFaseSelecaoPorBolaoUsuario(bolaoUsuario.Id);
                await palpiteRepositorio.DeletarPalpiteTerceiroLugarPorBolaoUsuario(bolaoUsuario.Id);

                foreach (var item in request)
                {
                    Selecao selecao = selecoesRepositorio.Recuperar(item.IdSelecao) ?? throw new Exception("Seleção não encontrada.");

                    await palpiteRepositorio.InserirPalpiteTerceiroLugar(new PalpiteTerceiroLugar(selecao, bolaoUsuario, item.Posicao));
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

        public async Task CriarPalpiteArtilheiroBrasil(CriarPalpiteArtilheiroBrasilRequest request)
        {
            ValidarPrazoParaPalpite();
            var transacao = session.BeginTransaction();
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(request.HashBolao));
                Usuario usuario = usuariosRepositorio.Recuperar(request.IdUsuario) ?? throw new Exception("Usuário não encontrado.");
                Jogador jogador = jogadoresRepositorio.Recuperar(request.JogadorId) ?? throw new Exception("Jogador não encontrado.");

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                await palpiteRepositorio.InserirPalpiteArtilheiroBrasil(new PalpiteArtilheiroBrasil(jogador, bolaoUsuario));

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

        public async Task<PalpiteArtilheiroBrasilResponse> RecuperarPalpiteArtilheiroBrasilAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                IQueryable<PalpiteArtilheiroBrasil> query = palpiteRepositorio.RecuperarQueryPalpiteArtilheiroBrasilPorBolaoUsuarioId(bolaoUsuario.Id);

                var projecao = query.Select(x => new PalpiteArtilheiroBrasilResponse
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
                    Selecao = new GrupoSelecaoResponse
                    {
                        Id = x.Selecao.Id,
                        Nome = x.Selecao.Nome,
                        Grupo = new GrupoResponse
                        {
                            Id = x.Selecao.Grupo.Id,
                            Nome = x.Selecao.Grupo.Nome
                        },
                        Logo = x.Selecao.Logo,
                        Abreviacao = x.Selecao.Abreviacao,
                        Pontuacao = x.Selecao.PontuacaoSelecao,
                        PosicaoFaseDeGrupos = x.Selecao.PosicaoFaseDeGrupos
                    },
                    PontuacaoSelecao = x.PontuacaoSelecao,
                    PosicaoSelecao = x.PosicaoSelecao
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
        public async Task<IList<PalpiteTerceiroLugarResponse>> RecuperarPalpitesTerceiroLugarAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                IQueryable<PalpiteTerceiroLugar> query = palpiteRepositorio.RecuperarQueryPalpiteTerceiroLugarPorBolaoUsuarioId(bolaoUsuario.Id);

                var projecao = query.Select(x => new PalpiteTerceiroLugarResponse
                {
                    Id = x.Id,
                    Posicao = x.Posicao,
                    Selecao = new GrupoSelecaoResponse
                    {
                        Id = x.Selecao.Id,
                        Nome = x.Selecao.Nome,
                        Grupo = new GrupoResponse { Id = x.Selecao.Grupo.Id, Nome = x.Selecao.Grupo.Nome },
                        Logo = x.Selecao.Logo,
                        Abreviacao = x.Selecao.Abreviacao,
                        Pontuacao = x.Selecao.PontuacaoSelecao,
                        PosicaoFaseDeGrupos = x.Selecao.PosicaoFaseDeGrupos
                    }
                });

                return await projecao.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar", ex);
            }
        }

    public async Task<IList<GrupoSelecaoResponse>> RecuperarPalpiteMelhoresTerceiroLugarAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                IQueryable<PalpiteGrupoSelecao> query = palpiteRepositorio.RecuperarQueryPalpiteGrupoSelecaoPorBolaoUsuarioId(bolaoUsuario.Id);

                var projecao = query
                    .Where(x => x.PosicaoSelecao == 3)
                    .Select(x => new GrupoSelecaoResponse
                    {
                        Id = x.Selecao.Id,
                        Nome = x.Selecao.Nome,
                        Grupo = new GrupoResponse
                        {
                            Id = x.Grupo.Id,
                            Nome = x.Grupo.Nome
                        },
                        Logo = x.Selecao.Logo,
                        Abreviacao = x.Selecao.Abreviacao,
                        Pontuacao = x.Selecao.PontuacaoSelecao,
                        PosicaoFaseDeGrupos = x.Selecao.PosicaoFaseDeGrupos
                    }).OrderBy(x => x.Pontuacao);

                var lista = await projecao.ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar", ex);
            }
        }

        public async Task<EliminatoriasResponse> RecuperarJogosEliminatoriasAsync(string hashBolao, int idUsuario)
        {
            try
            {
                var usuario = usuariosRepositorio.Recuperar(idUsuario) ?? throw new Exception("Usuário não encontrado.");
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                BolaoUsuario bolaoUsuario = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuario.Id) ?? throw new Exception("Bolao do usuario não encontrado.");

                // Recuperar palpites de grupo
                IQueryable<PalpiteGrupoSelecao> queryGrupo = palpiteRepositorio.RecuperarQueryPalpiteGrupoSelecaoPorBolaoUsuarioId(bolaoUsuario.Id);
                var palpitesGrupo = await queryGrupo.Select(x => new GrupoSelecaoResponse
                {
                    Id = x.Selecao.Id,
                    Nome = x.Selecao.Nome,
                    Grupo = new GrupoResponse { Id = x.Grupo.Id, Nome = x.Grupo.Nome },
                    Logo = x.Selecao.Logo,
                    Abreviacao = x.Selecao.Abreviacao,
                    Pontuacao = x.PontuacaoSelecao,
                    PosicaoFaseDeGrupos = x.PosicaoSelecao
                }).ToListAsync();

                // Recuperar palpites de terceiro lugar
                IQueryable<PalpiteTerceiroLugar> queryTerceiro = palpiteRepositorio.RecuperarQueryPalpiteTerceiroLugarPorBolaoUsuarioId(bolaoUsuario.Id);
                var palpitesTerceiro = await queryTerceiro.Select(x => new
                {
                    Selecao = new GrupoSelecaoResponse
                    {
                        Id = x.Selecao.Id,
                        Nome = x.Selecao.Nome,
                        Grupo = new GrupoResponse { Id = x.Selecao.Grupo.Id, Nome = x.Selecao.Grupo.Nome },
                        Logo = x.Selecao.Logo,
                        Abreviacao = x.Selecao.Abreviacao,
                        Pontuacao = x.Selecao.PontuacaoSelecao,
                        PosicaoFaseDeGrupos = 3
                    },
                    Posicao = x.Posicao
                }).ToListAsync();

                // Organizar classificados
                var classificados = new Dictionary<string, GrupoSelecaoResponse[]>();
                
                // Primeiro e segundo colocados por grupo (A-L)
                for (char grupo = 'A'; grupo <= 'L'; grupo++)
                {
                    var grupoNome = grupo.ToString();
                    var selecoesGrupo = palpitesGrupo
                        .Where(x => x.Grupo.Nome == grupoNome)
                        .OrderBy(x => x.PosicaoFaseDeGrupos)
                        .Take(2)
                        .ToArray();
                    
                    if (selecoesGrupo.Length == 2)
                    {
                        classificados[grupoNome] = selecoesGrupo;
                    }
                }

                // 8 melhores terceiros colocados com informação do grupo
                var melhoresTerceiros = palpitesTerceiro
                    .OrderBy(x => x.Posicao)
                    .Take(8)
                    .ToList();

                // Identificar quais grupos produziram os terceiros classificados
                var gruposTerceirosClassificados = melhoresTerceiros
                    .Select(x => x.Selecao.Grupo.Nome)
                    .OrderBy(x => x)
                    .ToList();

                // Criar dicionário de terceiros por grupo para fácil acesso
                var terceirosPorGrupo = melhoresTerceiros
                    .ToDictionary(x => x.Selecao.Grupo.Nome, x => x.Selecao);

                // Obter tabela de cruzamentos baseada na combinação de grupos
                var tabelaCruzamentos = ObterTabelaCruzamentosTerceiros(gruposTerceirosClassificados);

                // Montar jogos da Rodada de 16 (32 times = 16 jogos)
                // Chaveamento oficial da Copa 2026
                var rodadaDe16 = new List<JogoEliminatoriaResponse>();

                // CHAVE 1 (LADO A)
                // Jogo 1: 1º do Grupo E x 3º (definido pela tabela)
                var terceiro1 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo1"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(1, "Rodada de 16", ObterClassificado(classificados, "E", 1), terceiro1, 1));
                
                // Jogo 2: 1º do Grupo I x 3º (definido pela tabela)
                var terceiro2 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo2"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(2, "Rodada de 16", ObterClassificado(classificados, "I", 1), terceiro2, 1));
                
                // CHAVE 2 (LADO A)
                // Jogo 3: 2º do Grupo A x 2º do Grupo B
                rodadaDe16.Add(CriarJogo(3, "Rodada de 16", ObterClassificado(classificados, "A", 2), ObterClassificado(classificados, "B", 2), 2));
                
                // Jogo 4: 1º do Grupo F x 2º do Grupo C
                rodadaDe16.Add(CriarJogo(4, "Rodada de 16", ObterClassificado(classificados, "F", 1), ObterClassificado(classificados, "C", 2), 2));
                
                // CHAVE 3 (LADO A)
                // Jogo 5: 2º do Grupo K x 2º do Grupo L
                rodadaDe16.Add(CriarJogo(5, "Rodada de 16", ObterClassificado(classificados, "K", 2), ObterClassificado(classificados, "L", 2), 3));
                
                // Jogo 6: 1º do Grupo H x 2º do Grupo J
                rodadaDe16.Add(CriarJogo(6, "Rodada de 16", ObterClassificado(classificados, "H", 1), ObterClassificado(classificados, "J", 2), 3));
                
                // CHAVE 4 (LADO A)
                // Jogo 7: 1º do Grupo D x 3º (definido pela tabela)
                var terceiro7 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo7"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(7, "Rodada de 16", ObterClassificado(classificados, "D", 1), terceiro7, 4));
                
                // Jogo 8: 1º do Grupo G x 3º (definido pela tabela)
                var terceiro8 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo8"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(8, "Rodada de 16", ObterClassificado(classificados, "G", 1), terceiro8, 4));
                
                // CHAVE 5 (LADO B)
                // Jogo 9: 1º do Grupo C x 2º do Grupo F
                rodadaDe16.Add(CriarJogo(9, "Rodada de 16", ObterClassificado(classificados, "C", 1), ObterClassificado(classificados, "F", 2), 5));
                
                // Jogo 10: 2º do Grupo E x 2º do Grupo I
                rodadaDe16.Add(CriarJogo(10, "Rodada de 16", ObterClassificado(classificados, "E", 2), ObterClassificado(classificados, "I", 2), 5));
                
                // CHAVE 6 (LADO B)
                // Jogo 11: 1º do Grupo A x 3º (definido pela tabela)
                var terceiro11 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo11"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(11, "Rodada de 16", ObterClassificado(classificados, "A", 1), terceiro11, 6));
                
                // Jogo 12: 1º do Grupo L x 3º (definido pela tabela)
                var terceiro12 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo12"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(12, "Rodada de 16", ObterClassificado(classificados, "L", 1), terceiro12, 6));
                
                // CHAVE 7 (LADO B)
                // Jogo 13: 1º do Grupo J x 2º do Grupo H
                rodadaDe16.Add(CriarJogo(13, "Rodada de 16", ObterClassificado(classificados, "J", 1), ObterClassificado(classificados, "H", 2), 7));
                
                // Jogo 14: 2º do Grupo D x 2º do Grupo G
                rodadaDe16.Add(CriarJogo(14, "Rodada de 16", ObterClassificado(classificados, "D", 2), ObterClassificado(classificados, "G", 2), 7));
                
                // CHAVE 8 (LADO B)
                // Jogo 15: 1º do Grupo B x 3º (definido pela tabela)
                var terceiro15 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo15"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(15, "Rodada de 16", ObterClassificado(classificados, "B", 1), terceiro15, 8));
                
                // Jogo 16: 1º do Grupo K x 3º (definido pela tabela)
                var terceiro16 = ObterTerceiroPorCodigo(tabelaCruzamentos.GetValueOrDefault("Jogo16"), terceirosPorGrupo);
                rodadaDe16.Add(CriarJogo(16, "Rodada de 16", ObterClassificado(classificados, "K", 1), terceiro16, 8));

                // Recuperar palpites de fases eliminatórias (se existirem)
                IQueryable<PalpiteFaseSelecao> queryFaseSelecao = palpiteRepositorio.RecuperarQueryPalpiteFaseSelecaoPorBolaoUsuarioId(bolaoUsuario.Id);
                var palpitesFaseSelecao = await queryFaseSelecao.Select(x => new
                {
                    FaseId = x.Fase.Id,
                    FaseNome = x.Fase.Nome,
                    Selecao = new GrupoSelecaoResponse
                    {
                        Id = x.Selecao.Id,
                        Nome = x.Selecao.Nome,
                        Grupo = x.Selecao.Grupo != null ? new GrupoResponse { Id = x.Selecao.Grupo.Id, Nome = x.Selecao.Grupo.Nome } : null!,
                        Logo = x.Selecao.Logo,
                        Abreviacao = x.Selecao.Abreviacao,
                        Pontuacao = x.Selecao.PontuacaoSelecao,
                        PosicaoFaseDeGrupos = x.Selecao.PosicaoFaseDeGrupos
                    }
                }).ToListAsync();

                // Oitavas de final (8 chaves = 8 jogos)
                // Buscar palpites do usuário para Oitavas (Fase ID = 2)
                var palpitesOitavas = palpitesFaseSelecao.Where(x => x.FaseId == 2).Select(x => x.Selecao).ToList();
                var oitavas = new List<JogoEliminatoriaResponse>
                {
                    new JogoEliminatoriaResponse { NumeroJogo = 1, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(0), Selecao2 = palpitesOitavas.ElementAtOrDefault(1), ProximoJogoVencedor = 1 },
                    new JogoEliminatoriaResponse { NumeroJogo = 2, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(2), Selecao2 = palpitesOitavas.ElementAtOrDefault(3), ProximoJogoVencedor = 1 },
                    new JogoEliminatoriaResponse { NumeroJogo = 3, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(4), Selecao2 = palpitesOitavas.ElementAtOrDefault(5), ProximoJogoVencedor = 2 },
                    new JogoEliminatoriaResponse { NumeroJogo = 4, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(6), Selecao2 = palpitesOitavas.ElementAtOrDefault(7), ProximoJogoVencedor = 2 },
                    new JogoEliminatoriaResponse { NumeroJogo = 5, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(8), Selecao2 = palpitesOitavas.ElementAtOrDefault(9), ProximoJogoVencedor = 3 },
                    new JogoEliminatoriaResponse { NumeroJogo = 6, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(10), Selecao2 = palpitesOitavas.ElementAtOrDefault(11), ProximoJogoVencedor = 3 },
                    new JogoEliminatoriaResponse { NumeroJogo = 7, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(12), Selecao2 = palpitesOitavas.ElementAtOrDefault(13), ProximoJogoVencedor = 4 },
                    new JogoEliminatoriaResponse { NumeroJogo = 8, Fase = "Oitavas", Selecao1 = palpitesOitavas.ElementAtOrDefault(14), Selecao2 = palpitesOitavas.ElementAtOrDefault(15), ProximoJogoVencedor = 4 }
                };

                // Quartas de final (4 jogos)
                // Buscar palpites do usuário para Quartas (Fase ID = 3)
                var palpitesQuartas = palpitesFaseSelecao.Where(x => x.FaseId == 3).Select(x => x.Selecao).ToList();
                var quartas = new List<JogoEliminatoriaResponse>
                {
                    new JogoEliminatoriaResponse { NumeroJogo = 1, Fase = "Quartas", Selecao1 = palpitesQuartas.ElementAtOrDefault(0), Selecao2 = palpitesQuartas.ElementAtOrDefault(1), ProximoJogoVencedor = 1 },
                    new JogoEliminatoriaResponse { NumeroJogo = 2, Fase = "Quartas", Selecao1 = palpitesQuartas.ElementAtOrDefault(2), Selecao2 = palpitesQuartas.ElementAtOrDefault(3), ProximoJogoVencedor = 1 },
                    new JogoEliminatoriaResponse { NumeroJogo = 3, Fase = "Quartas", Selecao1 = palpitesQuartas.ElementAtOrDefault(4), Selecao2 = palpitesQuartas.ElementAtOrDefault(5), ProximoJogoVencedor = 2 },
                    new JogoEliminatoriaResponse { NumeroJogo = 4, Fase = "Quartas", Selecao1 = palpitesQuartas.ElementAtOrDefault(6), Selecao2 = palpitesQuartas.ElementAtOrDefault(7), ProximoJogoVencedor = 2 }
                };

                // Semifinais (2 jogos)
                // Buscar palpites do usuário para Semis (Fase ID = 4)
                var palpitesSemis = palpitesFaseSelecao.Where(x => x.FaseId == 4).Select(x => x.Selecao).ToList();
                var semis = new List<JogoEliminatoriaResponse>
                {
                    new JogoEliminatoriaResponse { NumeroJogo = 1, Fase = "Semis", Selecao1 = palpitesSemis.ElementAtOrDefault(0), Selecao2 = palpitesSemis.ElementAtOrDefault(1), ProximoJogoVencedor = 1 },
                    new JogoEliminatoriaResponse { NumeroJogo = 2, Fase = "Semis", Selecao1 = palpitesSemis.ElementAtOrDefault(2), Selecao2 = palpitesSemis.ElementAtOrDefault(3), ProximoJogoVencedor = 1 }
                };

                // Final
                // Buscar palpites do usuário para Finais (Fase ID = 5)
                var palpitesFinais = palpitesFaseSelecao.Where(x => x.FaseId == 5).Select(x => x.Selecao).ToList();
                var finais = new JogoEliminatoriaResponse 
                { 
                    NumeroJogo = 1, 
                    Fase = "Finais", 
                    Selecao1 = palpitesFinais.ElementAtOrDefault(0),
                    Selecao2 = palpitesFinais.ElementAtOrDefault(1),
                    ProximoJogoVencedor = null 
                };

                // Disputa de Terceiro Lugar
                // Buscar palpites do usuário para Disputa de Terceiro (Fase ID = 6)
                var palpitesDisputaTerceiro = palpitesFaseSelecao.Where(x => x.FaseId == 6).Select(x => x.Selecao).ToList();
                var disputaDeTerceiro = new JogoEliminatoriaResponse 
                { 
                    NumeroJogo = 1, 
                    Fase = "Disputa de terceiro", 
                    Selecao1 = palpitesDisputaTerceiro.ElementAtOrDefault(0),
                    Selecao2 = palpitesDisputaTerceiro.ElementAtOrDefault(1),
                    ProximoJogoVencedor = null 
                };

                // Campeão - Buscar palpite do usuário para Campeão (Fase ID = 7)
                var campeao = palpitesFaseSelecao.Where(x => x.FaseId == 7).Select(x => x.Selecao).FirstOrDefault();

                // Terceiro Lugar - Buscar palpite do usuário para Terceiro Lugar (Fase ID = 8)
                var terceiroLugar = palpitesFaseSelecao.Where(x => x.FaseId == 8).Select(x => x.Selecao).FirstOrDefault();

                return new EliminatoriasResponse
                {
                    RodadaDe16 = rodadaDe16,
                    Oitavas = oitavas,
                    Quartas = quartas,
                    Semis = semis,
                    Finais = finais,
                    DisputaDeTerceiro = disputaDeTerceiro,
                    Campeao = campeao,
                    TerceiroLugar = terceiroLugar
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar jogos das eliminatórias", ex);
            }
        }

        private GrupoSelecaoResponse? ObterClassificado(Dictionary<string, GrupoSelecaoResponse[]> classificados, string grupo, int posicao)
        {
            if (classificados.TryGetValue(grupo, out var selecoes) && selecoes.Length >= posicao)
            {
                return selecoes[posicao - 1];
            }
            return null;
        }

        private JogoEliminatoriaResponse CriarJogo(int numero, string fase, GrupoSelecaoResponse? selecao1, GrupoSelecaoResponse? selecao2, int? proximoJogo)
        {
            return new JogoEliminatoriaResponse
            {
                NumeroJogo = numero,
                Fase = fase,
                Selecao1 = selecao1,
                Selecao2 = selecao2,
                ProximoJogoVencedor = proximoJogo
            };
        }

        /// <summary>
        /// Obtém a tabela de cruzamentos dos terceiros colocados baseada na combinação de grupos classificados.
        /// NOTA: Esta é uma tabela simulada. Quando a FIFA divulgar a tabela oficial para Copa 2026,
        /// esta implementação deverá ser substituída pelos dados oficiais no arquivo TabelaCruzamentosTerceiros.json
        /// </summary>
        private Dictionary<string, string> ObterTabelaCruzamentosTerceiros(List<string> gruposClassificados)
        {
            // Criar chave única da combinação (ex: "A,B,C,D,E,F,G,H")
            var chaveCombinacao = string.Join(",", gruposClassificados);

            try
            {
                // Caminho para o arquivo JSON
                var caminhoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "TabelaCruzamentosTerceiros.json");

                // Verificar se o arquivo existe
                if (!File.Exists(caminhoJson))
                {
                    return CriarTabelaFallback(gruposClassificados);
                }

                // Ler e deserializar o JSON
                var jsonString = File.ReadAllText(caminhoJson);
                var tabelasOficiais = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, CruzamentoJogo>>>(jsonString);

                if (tabelasOficiais == null || !tabelasOficiais.TryGetValue(chaveCombinacao, out var tabelaCruzamentos))
                {
                    return CriarTabelaFallback(gruposClassificados);
                }

                // Converter para o formato esperado (apenas o Away, pois Home já está fixo no código)
                return tabelaCruzamentos.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Away
                );
            }
            catch (Exception)
            {
                // Em caso de erro, usar fallback
                return CriarTabelaFallback(gruposClassificados);
            }
        }

        /// <summary>
        /// Cria tabela de fallback quando a combinação não está mapeada
        /// </summary>
        private Dictionary<string, string> CriarTabelaFallback(List<string> gruposClassificados)
        {
            return new Dictionary<string, string>
            {
                ["Jogo1"] = gruposClassificados.Count > 0 ? $"3{gruposClassificados[0]}" : string.Empty,
                ["Jogo2"] = gruposClassificados.Count > 1 ? $"3{gruposClassificados[1]}" : string.Empty,
                ["Jogo7"] = gruposClassificados.Count > 2 ? $"3{gruposClassificados[2]}" : string.Empty,
                ["Jogo8"] = gruposClassificados.Count > 3 ? $"3{gruposClassificados[3]}" : string.Empty,
                ["Jogo11"] = gruposClassificados.Count > 4 ? $"3{gruposClassificados[4]}" : string.Empty,
                ["Jogo12"] = gruposClassificados.Count > 5 ? $"3{gruposClassificados[5]}" : string.Empty,
                ["Jogo15"] = gruposClassificados.Count > 6 ? $"3{gruposClassificados[6]}" : string.Empty,
                ["Jogo16"] = gruposClassificados.Count > 7 ? $"3{gruposClassificados[7]}" : string.Empty
            };
        }

        /// <summary>
        /// Classe auxiliar para deserialização do JSON
        /// </summary>
        private class CruzamentoJogo
        {
            public string Home { get; set; } = string.Empty;
            public string Away { get; set; } = string.Empty;
        }

        /// <summary>
        /// Obtém a seleção terceira colocada com base no código (ex: "3A" retorna o terceiro do grupo A)
        /// </summary>
        private GrupoSelecaoResponse? ObterTerceiroPorCodigo(string? codigo, Dictionary<string, GrupoSelecaoResponse> terceirosPorGrupo)
        {
            if (string.IsNullOrEmpty(codigo) || !codigo.StartsWith("3"))
                return null;

            var grupo = codigo.Substring(1); // Remove o "3" e pega a letra do grupo
            return terceirosPorGrupo.GetValueOrDefault(grupo);
        }

        public async Task<PalpitesPorEscritoResponse> ObterPalpitesPorEscrito(string nomeUsuario, string hashBolao, int idUsuarioSolicitante)
        {
            try
            {
                int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

                // Verificar se usuário solicitante faz parte do bolão
                var bolaoUsuarioSolicitante = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, idUsuarioSolicitante);
                if (bolaoUsuarioSolicitante == null)
                {
                    throw new UnauthorizedAccessException("Você não faz parte deste bolão.");
                }

                // Buscar usuário solicitado pelo nome
                var queryUsuario = usuariosRepositorio.Query();
                var usuarioSolicitado = await queryUsuario.Where(x => x.Nome == nomeUsuario).FirstOrDefaultAsync();
                
                if (usuarioSolicitado == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                // Verificar se usuário solicitado faz parte do bolão
                var bolaoUsuarioSolicitado = await boloesUsuariosRepositorio.RecuperarAsync(idBolao, usuarioSolicitado.Id);
                if (bolaoUsuarioSolicitado == null)
                {
                    throw new UnauthorizedAccessException("O usuário solicitado não faz parte deste bolão.");
                }

                var response = new PalpitesPorEscritoResponse();

                // Palpite Fase Grupos
                var palpitesGrupos = await RecuperarPalpiteGrupoSelecaoAsync(hashBolao, usuarioSolicitado.Id);
                if (palpitesGrupos != null && palpitesGrupos.Any())
                {
                    var gruposAgrupados = palpitesGrupos.GroupBy(p => p.Selecao.Grupo.Nome).OrderBy(g => g.Key);
                    var textoGrupos = string.Join(", ", gruposAgrupados.Select(grupo =>
                    {
                        var selecoes = grupo.OrderBy(s => s.PosicaoSelecao).Select(s => 
                            $"{s.Selecao.Nome} {s.PosicaoSelecao}º {s.PontuacaoSelecao ?? 0}pts"
                        );
                        return $"Grupo {grupo.Key}: {string.Join(", ", selecoes)}";
                    }));
                    response.PalpiteFaseGrupos = $"{textoGrupos}";
                }

                // Palpite Eliminatórias
                var palpitesFases = await RecuperarPalpiteFaseSelecaoAsync(hashBolao, usuarioSolicitado.Id);
                if (palpitesFases != null && palpitesFases.Any())
                {
                    var fasesAgrupadas = palpitesFases.GroupBy(p => p.Fase.Nome).OrderBy(f => f.Key);
                    var textoFases = string.Join(", ", fasesAgrupadas.Select(fase =>
                    {
                        var selecoes = string.Join(", ", fase.Select(s => s.Selecao.Nome));
                        return $"{fase.Key}: {selecoes}";
                    }));
                    response.PalpiteEliminatorias = $"{textoFases}";
                }

                // Palpite Melhores Terceiros
                var palpitesTerceiros = await RecuperarPalpitesTerceiroLugarAsync(hashBolao, usuarioSolicitado.Id);
                if (palpitesTerceiros != null && palpitesTerceiros.Any())
                {
                    var textoTerceiros = string.Join(", ", palpitesTerceiros
                        .OrderBy(t => t.Posicao)
                        .Select(t => $"{t.Selecao.Nome} {t.Posicao}º"));
                    response.PalpiteMelhoresTerceiros = $"{textoTerceiros}";
                }

                // Palpite Artilheiro
                var palpiteArtilheiro = await RecuperarPalpiteArtilheiroAsync(hashBolao, usuarioSolicitado.Id);
                if (palpiteArtilheiro != null && !string.IsNullOrEmpty(palpiteArtilheiro.JogadorNome))
                {
                    response.PalpiteArtilheiro = $"{palpiteArtilheiro.JogadorNome}";
                }

                // Palpite Resultados Jogos do Brasil
                var palpitesJogosGrupo = await RecuperarPalpiteJogoGrupoAsync(hashBolao, usuarioSolicitado.Id);
                var jogosBrasil = palpitesJogosGrupo?
                    .Where(p => p.Selecao1?.Abreviacao == "BRA" || p.Selecao2?.Abreviacao == "BRA")
                    .ToList();
                if (jogosBrasil != null && jogosBrasil.Any())
                {
                    var textoJogosBrasil = string.Join(", ", jogosBrasil.Select(j =>
                        $"{j.Selecao1?.Nome} {j.PlacarSelecao1} x {j.PlacarSelecao2} {j.Selecao2?.Nome}"));
                    response.PalpiteResultadosJogosBrasil = textoJogosBrasil;
                }

                // Palpite Artilheiro Brasil
                var palpiteArtilheiroBrasil = await RecuperarPalpiteArtilheiroBrasilAsync(hashBolao, usuarioSolicitado.Id);
                if (palpiteArtilheiroBrasil != null && !string.IsNullOrEmpty(palpiteArtilheiroBrasil.JogadorNome))
                {
                    response.PalpiteArtilheiroBrasil = palpiteArtilheiroBrasil.JogadorNome;
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

    }

}

