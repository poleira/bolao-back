using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;
using BolaoTeste.Dto.Rank;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.Rank.Servicos
{
    public class RankServico : IRankServico
    {
        private static readonly Dictionary<string, int> DescricaoParaFaseMap = new Dictionary<string, int>
        {
            ["acertou pais nas 1/16 avos de final"] = 1,
            ["acertou pais nas oitavas"] = 2,
            ["acertou pais quartas"] = 3,
            ["acertou pais semis"] = 4,
            ["acertou pais final"] = 5,
            ["acertou campeao"] = 7,
            ["acertou terceiro colocado geral da copa"] = 8
        };

        private static readonly Dictionary<string, GroupStageRuleType> DescricaoParaRegraGrupoMap = new Dictionary<string, GroupStageRuleType>
        {
            ["acertou a posicao do pais na fase de grupos"] = GroupStageRuleType.Position,
            ["acertou a pontuacao do pais na fase de grupos"] = GroupStageRuleType.Points
        };

        private static readonly HashSet<string> DescricaoRegraArtilheiroSet = new HashSet<string>
        {
            "acertou artilheiro"
        };

        private static readonly HashSet<string> DescricaoRegraArtilheiroBrasilSet = new HashSet<string>
        {
            "acertou artilheiro do brasil"
        };

        private static readonly HashSet<string> DescricaoRegraMelhorTerceiroSet = new HashSet<string>
        {
            "acertou posicao do terceiro colocado classificado para eliminatoria"
        };

        private static readonly HashSet<string> DescricaoRegraAcertouPlacarBrasilSet = new HashSet<string>
        {
            "acertou o placar do brasil"
        };

        private readonly ISession session;
        private readonly IBoloesRepositorio boloesRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;

        public RankServico(
            ISession session,
            IBoloesRepositorio boloesRepositorio,
            IBoloesUsuariosRepositorio boloesUsuariosRepositorio)
        {
            this.session = session;
            this.boloesRepositorio = boloesRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
        }

        public async Task<IList<RankResponse>> ListarRankAsync(string hashBolao)
        {
            if (string.IsNullOrWhiteSpace(hashBolao))
            {
                throw new ArgumentException("HashBolao inválido.", nameof(hashBolao));
            }

            int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

            var configuracaoRegras = await ObterConfiguracaoRegrasAsync(idBolao);
            var regrasPorFase = configuracaoRegras.PontuacaoPorFase;
            var participantes = await boloesUsuariosRepositorio.Query()
                .Where(x => x.Bolao.Id == idBolao)
                .Select(x => new { x.Id, UsuarioNome = x.Usuario.Nome, IdUsuario = x.Usuario.Id })
                .ToListAsync();

            var acumuladores = participantes.ToDictionary(
                x => x.Id,
                x => new RankAccumulator(x.UsuarioNome, x.IdUsuario));

            if (!acumuladores.Any())
            {
                return new List<RankResponse>();
            }

            var possuiRegrasEliminatorias = regrasPorFase.Any();
            var possuiRegrasGrupos = configuracaoRegras.PontosAcertoPosicaoGrupo.HasValue || configuracaoRegras.PontosAcertoPontuacaoGrupo.HasValue;
            var possuiRegraArtilheiro = configuracaoRegras.PontosAcertoArtilheiro.HasValue;
            var possuiRegraArtilheiroBrasil = configuracaoRegras.PontosAcertoArtilheiroBrasil.HasValue;
            var possuiRegraMelhorTerceiro = configuracaoRegras.PontosAcertoMelhorTerceiro.HasValue;
            var possuiRegraAcertouPlacarBrasil = configuracaoRegras.PontosAcertoPlacarBrasil.HasValue;

            if (!possuiRegrasEliminatorias && !possuiRegrasGrupos && !possuiRegraArtilheiro && !possuiRegraArtilheiroBrasil && !possuiRegraMelhorTerceiro && !possuiRegraAcertouPlacarBrasil)
            {
                return ConverterParaResponse(acumuladores);
            }

            if (possuiRegrasEliminatorias)
            {
                await AplicarPontuacaoEliminatoriasAsync(idBolao, regrasPorFase, acumuladores);
            }

            if (possuiRegrasGrupos)
            {
                await AplicarPontuacaoFaseDeGruposAsync(idBolao, configuracaoRegras, acumuladores);
            }

            if (possuiRegraArtilheiro)
            {
                await AplicarPontuacaoArtilheiroAsync(idBolao, configuracaoRegras.PontosAcertoArtilheiro!.Value, acumuladores);
            }

            if (possuiRegraArtilheiroBrasil)
            {
                await AplicarPontuacaoArtilheiroBrasilAsync(idBolao, configuracaoRegras.PontosAcertoArtilheiroBrasil!.Value, acumuladores);
            }

            if (possuiRegraMelhorTerceiro)
            {
                await AplicarPontuacaoMelhorTerceiroAsync(idBolao, configuracaoRegras.PontosAcertoMelhorTerceiro!.Value, acumuladores);
            }

            if (possuiRegraAcertouPlacarBrasil)
            {
                await AplicarPontuacaoPlacarBrasilAsync(idBolao, configuracaoRegras.PontosAcertoPlacarBrasil!.Value, acumuladores);
            }

            return ConverterParaResponse(acumuladores);
        }

        private async Task<RankRulesConfig> ObterConfiguracaoRegrasAsync(int idBolao)
        {
            var regras = await boloesRepositorio.QueryBolaoRegra()
                .Where(x => x.Bolao.Id == idBolao)
                .Select(x => new { x.Pontuacao, x.Regra.Descricao })
                .ToListAsync();

            var resultado = new RankRulesConfig();
            foreach (var regra in regras)
            {
                var faseId = MapearDescricaoParaFase(regra.Descricao);
                if (!faseId.HasValue)
                {
                    var regraGrupo = MapearDescricaoParaRegraDeGrupo(regra.Descricao);
                    if (!regraGrupo.HasValue)
                    {
                        var handled = false;
                        if (DescricaoSeRefereAoArtilheiro(regra.Descricao))
                        {
                            resultado.PontosAcertoArtilheiro = regra.Pontuacao;
                            handled = true;
                        }

                        if (DescricaoSeRefereAoArtilheiroBrasil(regra.Descricao))
                        {
                            resultado.PontosAcertoArtilheiroBrasil = regra.Pontuacao;
                            handled = true;
                        }

                        if (DescricaoSeRefereAoMelhorTerceiro(regra.Descricao))
                        {
                            resultado.PontosAcertoMelhorTerceiro = regra.Pontuacao;
                            handled = true;
                        }

                        if (DescricaoSeRefereAoPlacarDoBrasil(regra.Descricao))
                        {
                            resultado.PontosAcertoPlacarBrasil = regra.Pontuacao;
                            handled = true;
                        }

                        if (!handled)
                        {
                            continue;
                        }

                        continue;
                    }

                    switch (regraGrupo.Value)
                    {
                        case GroupStageRuleType.Position:
                            resultado.PontosAcertoPosicaoGrupo = regra.Pontuacao;
                            break;
                        case GroupStageRuleType.Points:
                            resultado.PontosAcertoPontuacaoGrupo = regra.Pontuacao;
                            break;
                    }
                    continue;
                }

                resultado.PontuacaoPorFase[faseId.Value] = regra.Pontuacao;
            }

            return resultado;
        }

        private async Task AplicarPontuacaoEliminatoriasAsync(int idBolao, Dictionary<int, int> regrasPorFase, Dictionary<int, RankAccumulator> acumuladores)
        {
            if (!regrasPorFase.Any())
            {
                return;
            }

            var faseIds = regrasPorFase.Keys.ToList();
            var gabaritoPorFase = await ObterGabaritoPorFaseAsync(faseIds);

            if (!gabaritoPorFase.Any())
            {
                return;
            }

            var palpites = await session.Query<PalpiteFaseSelecao>()
                .Where(p => p.BolaoUsuario.Bolao.Id == idBolao)
                .Where(p => faseIds.Contains(p.Fase.Id))
                .Select(p => new
                {
                    BolaoUsuarioId = p.BolaoUsuario.Id,
                    FaseId = p.Fase.Id,
                    SelecaoId = p.Selecao.Id
                })
                .ToListAsync();

            if (!palpites.Any())
            {
                return;
            }

            foreach (var grupoUsuario in palpites.GroupBy(x => x.BolaoUsuarioId))
            {
                if (!acumuladores.TryGetValue(grupoUsuario.Key, out var accumulator))
                {
                    continue;
                }

                foreach (var grupoFase in grupoUsuario.GroupBy(x => x.FaseId))
                {
                    if (!regrasPorFase.TryGetValue(grupoFase.Key, out var pontosPorAcerto))
                    {
                        continue;
                    }

                    if (!gabaritoPorFase.TryGetValue(grupoFase.Key, out var gabaritoSelecoes))
                    {
                        continue;
                    }

                    var acertos = grupoFase
                        .Select(x => x.SelecaoId)
                        .Distinct()
                        .Count(id => gabaritoSelecoes.Contains(id));

                    accumulator.Pontuacao += acertos * pontosPorAcerto;
                }
            }
        }

        private async Task AplicarPontuacaoFaseDeGruposAsync(int idBolao, RankRulesConfig configuracao, Dictionary<int, RankAccumulator> acumuladores)
        {
            var considerarPosicao = configuracao.PontosAcertoPosicaoGrupo.HasValue;
            var considerarPontuacao = configuracao.PontosAcertoPontuacaoGrupo.HasValue;

            if (!considerarPosicao && !considerarPontuacao)
            {
                return;
            }

            var palpites = await session.Query<PalpiteGrupoSelecao>()
                .Where(p => p.BolaoUsuario.Bolao.Id == idBolao)
                .Select(p => new
                {
                    BolaoUsuarioId = p.BolaoUsuario.Id,
                    SelecaoId = p.Selecao.Id,
                    PosicaoPalpite = p.PosicaoSelecao,
                    PontuacaoPalpite = p.PontuacaoSelecao
                })
                .ToListAsync();

            if (!palpites.Any())
            {
                return;
            }

            var selecoes = await session.Query<Selecao>()
                .Select(s => new
                {
                    s.Id,
                    s.PosicaoFaseDeGrupos,
                    s.PontuacaoSelecao
                })
                .ToListAsync();

            var gabarito = selecoes.ToDictionary(
                x => x.Id,
                x => new GrupoSelecaoGabarito
                {
                    Posicao = x.PosicaoFaseDeGrupos,
                    Pontuacao = x.PontuacaoSelecao
                });

            foreach (var palpite in palpites)
            {
                if (!acumuladores.TryGetValue(palpite.BolaoUsuarioId, out var accumulator))
                {
                    continue;
                }

                if (!gabarito.TryGetValue(palpite.SelecaoId, out var dadosGabarito))
                {
                    continue;
                }

                if (considerarPosicao
                    && dadosGabarito.Posicao.HasValue
                    && palpite.PosicaoPalpite > 0
                    && dadosGabarito.Posicao.Value == palpite.PosicaoPalpite)
                {
                    accumulator.Pontuacao += configuracao.PontosAcertoPosicaoGrupo!.Value;
                }

                if (considerarPontuacao
                    && dadosGabarito.Pontuacao.HasValue
                    && palpite.PontuacaoPalpite.HasValue
                    && dadosGabarito.Pontuacao.Value == palpite.PontuacaoPalpite.Value)
                {
                    accumulator.Pontuacao += configuracao.PontosAcertoPontuacaoGrupo!.Value;
                }
            }
        }

        private async Task AplicarPontuacaoArtilheiroAsync(int idBolao, int pontosPorAcerto, Dictionary<int, RankAccumulator> acumuladores)
        {
            var palpites = await session.Query<PalpiteArtilheiro>()
                .Where(p => p.BolaoUsuario.Bolao.Id == idBolao)
                .Select(p => new
                {
                    BolaoUsuarioId = p.BolaoUsuario.Id,
                    JogadorId = p.Jogador.Id
                })
                .ToListAsync();

            if (!palpites.Any())
            {
                return;
            }

            var artilheiros = await session.Query<Artilheiro>()
                .Select(a => a.Jogador.Id)
                .ToListAsync();

            if (!artilheiros.Any())
            {
                return;
            }

            var gabarito = new HashSet<int>(artilheiros);

            foreach (var grupoUsuario in palpites.GroupBy(x => x.BolaoUsuarioId))
            {
                if (!acumuladores.TryGetValue(grupoUsuario.Key, out var accumulator))
                {
                    continue;
                }

                if (grupoUsuario.Any(x => gabarito.Contains(x.JogadorId)))
                {
                    accumulator.Pontuacao += pontosPorAcerto;
                }
            }
        }

        private async Task AplicarPontuacaoArtilheiroBrasilAsync(int idBolao, int pontosPorAcerto, Dictionary<int, RankAccumulator> acumuladores)
        {
            var palpites = await session.Query<PalpiteArtilheiroBrasil>()
                .Where(p => p.BolaoUsuario.Bolao.Id == idBolao)
                .Select(p => new
                {
                    BolaoUsuarioId = p.BolaoUsuario.Id,
                    JogadorId = p.Jogador.Id
                })
                .ToListAsync();

            if (!palpites.Any())
            {
                return;
            }

            var artilheiros = await session.Query<Artilheiro>()
                .Select(a => a.Jogador.Id)
                .ToListAsync();

            if (!artilheiros.Any())
            {
                return;
            }

            var gabarito = new HashSet<int>(artilheiros);

            foreach (var grupoUsuario in palpites.GroupBy(x => x.BolaoUsuarioId))
            {
                if (!acumuladores.TryGetValue(grupoUsuario.Key, out var accumulator))
                {
                    continue;
                }

                if (grupoUsuario.Any(x => gabarito.Contains(x.JogadorId)))
                {
                    accumulator.Pontuacao += pontosPorAcerto;
                }
            }
        }

        private async Task AplicarPontuacaoPlacarBrasilAsync(int idBolao, int pontosPorAcerto, Dictionary<int, RankAccumulator> acumuladores)
        {
            var jogosDoBrasil = await session.Query<JogoGrupo>()
                .Where(j => j.Selecao1.Abreviacao == "BRA" || j.Selecao2.Abreviacao == "BRA")
                .Select(j => new
                {
                    j.Id,
                    Selecao1Id = j.Selecao1.Id,
                    Selecao2Id = j.Selecao2.Id,
                    j.PlacarSelecao1,
                    j.PlacarSelecao2
                })
                .ToListAsync();

            var jogosComPlacar = jogosDoBrasil
                .Where(j => j.PlacarSelecao1.HasValue && j.PlacarSelecao2.HasValue)
                .ToList();

            if (!jogosComPlacar.Any())
            {
                return;
            }

            var palpites = await session.Query<PalpiteJogoGrupo>()
                .Where(p => p.BolaoUsuario.Bolao.Id == idBolao)
                .Select(p => new
                {
                    BolaoUsuarioId = p.BolaoUsuario.Id,
                    Selecao1Id = p.Selecao1.Id,
                    Selecao2Id = p.Selecao2.Id,
                    p.PlacarSelecao1,
                    p.PlacarSelecao2
                })
                .ToListAsync();

            if (!palpites.Any())
            {
                return;
            }

            foreach (var grupoUsuario in palpites.GroupBy(x => x.BolaoUsuarioId))
            {
                if (!acumuladores.TryGetValue(grupoUsuario.Key, out var accumulator))
                {
                    continue;
                }

                foreach (var jogo in jogosComPlacar)
                {
                    var palpite = grupoUsuario.FirstOrDefault(p =>
                        p.Selecao1Id == jogo.Selecao1Id && p.Selecao2Id == jogo.Selecao2Id);

                    if (palpite == null)
                    {
                        continue;
                    }

                    if (palpite.PlacarSelecao1 == jogo.PlacarSelecao1!.Value &&
                        palpite.PlacarSelecao2 == jogo.PlacarSelecao2!.Value)
                    {
                        accumulator.Pontuacao += pontosPorAcerto;
                    }
                }
            }
        }

        private async Task AplicarPontuacaoMelhorTerceiroAsync(int idBolao, int pontosPorAcerto, Dictionary<int, RankAccumulator> acumuladores)
        {
            var palpites = await session.Query<PalpiteTerceiroLugar>()
                .Where(p => p.BolaoUsuario.Bolao.Id == idBolao)
                .Select(p => new
                {
                    BolaoUsuarioId = p.BolaoUsuario.Id,
                    SelecaoId = p.Selecao.Id,
                    Posicao = p.Posicao
                })
                .ToListAsync();

            if (!palpites.Any())
            {
                return;
            }

            var melhoresTerceiros = await session.Query<MelhorTerceiroLugar>()
                .Select(mt => new
                {
                    mt.Posicao,
                    SelecaoId = mt.Selecao.Id
                })
                .ToListAsync();

            if (!melhoresTerceiros.Any())
            {
                return;
            }

            var gabaritoPorPosicao = melhoresTerceiros
                .Where(x => x.Posicao > 0)
                .GroupBy(x => x.Posicao)
                .ToDictionary(g => g.Key, g => g.First().SelecaoId);

            foreach (var palpite in palpites)
            {
                if (palpite.Posicao <= 0)
                {
                    continue;
                }

                if (!acumuladores.TryGetValue(palpite.BolaoUsuarioId, out var accumulator))
                {
                    continue;
                }

                if (!gabaritoPorPosicao.TryGetValue(palpite.Posicao, out var selecaoId))
                {
                    continue;
                }

                if (selecaoId == palpite.SelecaoId)
                {
                    accumulator.Pontuacao += pontosPorAcerto;
                }
            }
        }

        private async Task<Dictionary<int, HashSet<int>>> ObterGabaritoPorFaseAsync(IEnumerable<int> faseIds)
        {
            var resultado = new Dictionary<int, HashSet<int>>();

            foreach (var faseId in faseIds.Distinct())
            {
                var fase = await session.GetAsync<Fase>(faseId);
                if (fase?.Selecoes == null)
                {
                    continue;
                }

                var selecoes = fase.Selecoes
                    .Where(fs => fs?.Selecao != null)
                    .Select(fs => fs.Selecao.Id)
                    .ToHashSet();

                if (selecoes.Any())
                {
                    resultado[faseId] = selecoes;
                }
            }

            return resultado;
        }

        private static IList<RankResponse> ConverterParaResponse(Dictionary<int, RankAccumulator> acumuladores)
        {
            return acumuladores.Values
                .OrderByDescending(x => x.Pontuacao)
                .ThenBy(x => x.Usuario, StringComparer.OrdinalIgnoreCase)
                .Select(x => new RankResponse(x.Usuario, x.Pontuacao) { IdUsuario = x.IdUsuario })
                .ToList();
        }

        private static int? MapearDescricaoParaFase(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return null;
            }

            var normalizado = NormalizarDescricao(descricao);
            if (DescricaoParaFaseMap.TryGetValue(normalizado, out var faseId))
            {
                return faseId;
            }

            if (normalizado.Contains("1/16"))
            {
                return 1;
            }

            if (normalizado.Contains("oitav"))
            {
                return 2;
            }

            if (normalizado.Contains("quart"))
            {
                return 3;
            }

            if (normalizado.Contains("semi"))
            {
                return 4;
            }

            if (normalizado.Contains("campea"))
            {
                return 7;
            }

            if (normalizado.Contains("terceir"))
            {
                return 8;
            }

            if (normalizado.Contains("final"))
            {
                return 5;
            }

            return null;
        }

        private static bool DescricaoSeRefereAoArtilheiro(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return false;
            }

            var normalizado = NormalizarDescricao(descricao);
            if (DescricaoRegraArtilheiroSet.Contains(normalizado))
            {
                return true;
            }

            return normalizado.Contains("artilheiro");
        }

        private static bool DescricaoSeRefereAoArtilheiroBrasil(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return false;
            }

            var normalizado = NormalizarDescricao(descricao);
            if (DescricaoRegraArtilheiroBrasilSet.Contains(normalizado))
            {
                return true;
            }

            return normalizado.Contains("artilheiro") && normalizado.Contains("brasil");
        }

        private static bool DescricaoSeRefereAoMelhorTerceiro(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return false;
            }

            var normalizado = NormalizarDescricao(descricao).Replace("/", " ");
            if (DescricaoRegraMelhorTerceiroSet.Contains(normalizado))
            {
                return true;
            }

            return normalizado.Contains("terceiro") && normalizado.Contains("eliminatoria");
        }

        private static bool DescricaoSeRefereAoPlacarDoBrasil(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return false;
            }

            var normalizado = NormalizarDescricao(descricao);
            if (DescricaoRegraAcertouPlacarBrasilSet.Contains(normalizado))
            {
                return true;
            }

            return normalizado.Contains("placar") && normalizado.Contains("brasil");
        }

        private static GroupStageRuleType? MapearDescricaoParaRegraDeGrupo(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return null;
            }

            var normalizado = NormalizarDescricao(descricao);
            if (DescricaoParaRegraGrupoMap.TryGetValue(normalizado, out var regra))
            {
                return regra;
            }

            if (normalizado.Contains("grupo"))
            {
                if (normalizado.Contains("posicao"))
                {
                    return GroupStageRuleType.Position;
                }

                if (normalizado.Contains("pontuacao"))
                {
                    return GroupStageRuleType.Points;
                }
            }

            return null;
        }

        private static string NormalizarDescricao(string descricao)
        {
            var texto = descricao.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder(texto.Length);

            foreach (var caractere in texto)
            {
                var categoria = CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(char.ToLowerInvariant(caractere));
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC).Trim();
        }

        private class RankAccumulator
        {
            public RankAccumulator(string usuario, int idUsuario)
            {
                Usuario = usuario;
                IdUsuario = idUsuario;
            }

            public string Usuario { get; }
            public int IdUsuario { get; }
            public int Pontuacao { get; set; }
        }

        private sealed class RankRulesConfig
        {
            public Dictionary<int, int> PontuacaoPorFase { get; } = new Dictionary<int, int>();
            public int? PontosAcertoPosicaoGrupo { get; set; }
            public int? PontosAcertoPontuacaoGrupo { get; set; }
            public int? PontosAcertoArtilheiro { get; set; }
            public int? PontosAcertoArtilheiroBrasil { get; set; }
            public int? PontosAcertoMelhorTerceiro { get; set; }
            public int? PontosAcertoPlacarBrasil { get; set; }
        }

        private sealed class GrupoSelecaoGabarito
        {
            public int? Posicao { get; init; }
            public int? Pontuacao { get; init; }
        }

        private enum GroupStageRuleType
        {
            Position,
            Points
        }
    }
}
